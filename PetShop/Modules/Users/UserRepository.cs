using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Helpers;
using PetShop.Modules.Pets;
using PetShop.Modules.Users.Interfaces;
using PetShop.Token;

namespace PetShop.Modules.Users;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _context
        .Users
        .Where(t => t.Id == userId)
        .ExecuteDeleteAsync();

        await _context.SaveChangesAsync();
    }

    public async Task<UserModel> GetUserByIdAsync(int userId)
    {
        var user = await _context
                    .Users
                    .AsNoTracking()
                    .Select(u => new UserModel
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        Phone = u.Phone,
                        Address = u.Address,
                        Pets = u.Pets.Select(p => new PetModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Race = p.Race,
                            Size = p.Size,
                            OwnerId = p.OwnerId,
                            Owner = null
                        }).ToList()
                    }).FirstOrDefaultAsync(x => x.Id == userId);
        return user;
    }

    public async Task<IEnumerable<UserModel>> GetUsersAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<UserModel>> GetUsersAsync(int userToSearchId)
    {
        return await _context.Users.AsNoTracking().Where(x => x.Id == userToSearchId).ToListAsync();
    }

    public async Task<UserModel> InsertUserAsync(UserModel userModel)
    {
        var password = userModel.Password;

        userModel.Password = PasswordHelper.HashPassword(password);
        userModel.Role = "client";

        await _context.Users.AddAsync(userModel);
        await _context.SaveChangesAsync();

        return userModel;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _context
                    .Users
                    .AsNoTracking()
                    .Select(u => new UserModel
                    {
                        Id = u.Id,
                        Password = u.Password,
                        Email = u.Email,
                        Name = u.Name,
                        Role = u.Role
                    }).FirstOrDefaultAsync(x => x.Email == email);

        if (user is null)
            return null;

        var correctPassword = PasswordHelper.VerifyPassword(user.Password, password);

        if (!correctPassword)
            return null;

        var token = TokenServices.GenerateToken(user);

        return token;
    }

    public async Task<UserModel> UpdateUserAsync(int userId, UserModel userModel)
    {
        await _context
          .Users
          .Where(t => t.Id == userId)
          .ExecuteUpdateAsync(setters => setters
               .SetProperty(e => e.Name, userModel.Name)
               .SetProperty(e => e.Address, userModel.Address)
               .SetProperty(e => e.Phone, userModel.Phone)
               .SetProperty(e => e.Email, userModel.Email)
           );

        await _context.SaveChangesAsync();

        return userModel;
    }
}