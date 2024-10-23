using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Modules.Pets.Interfaces;
using PetShop.Modules.Scheduling;
using PetShop.Modules.Users;

namespace PetShop.Modules.Pets;

public class PetRepository : IPetRepository
{
    private readonly AppDbContext _context;

    public PetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeletePetAsync(int petId, int ownerId)
    {
        await _context
        .Pets
        .Where(t => t.Id == petId && t.OwnerId == ownerId)
        .ExecuteDeleteAsync();

        await _context.SaveChangesAsync();
    }

    public async Task<PetModel> GetPetByIdAsync(int petId)
    {
        var pet = await _context
                        .Pets
                        .AsNoTracking()
                        .Select(p => new PetModel
                        {
                             Id = p.Id,
                             Name = p.Name,
                             Size = p.Size,
                             OwnerId = p.OwnerId,
                             Race = p.Race,
                             Owner = new UserModel
                             {
                                 Id = p.Owner.Id,
                                 Name = p.Owner.Name,
                                 Email = p.Owner.Email,
                                 Phone = p.Owner.Phone,
                                 Address = p.Owner.Address,
                                 Pets = null
                             },
                            Schedulings = p.Schedulings.Select(p => new SchedulingModel
                            {
                                Id = p.Id,
                                Date = p.Date,
                                Time = p.Time,
                                PetId = p.PetId,
                                ServiceId = p.ServiceId,
                                Status = p.Status,
                                Pet = null,
                                Service = null
                            }).ToList()
                        })
                        .FirstOrDefaultAsync(x => x.Id == petId);
        return pet;
    }

    public async Task<PetModel> GetPetByIdAsync(int petId, int ownerId)
    {
        var pet = await _context
                        .Pets
                        .AsNoTracking()
                        .Where(x => x.OwnerId == ownerId)
                        .Select(p => new PetModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Size = p.Size,
                            OwnerId = p.OwnerId,
                            Race = p.Race,
                            Owner = new UserModel
                            {
                                Id = p.Owner.Id,
                                Name = p.Owner.Name,
                                Email = p.Owner.Email,
                                Phone = p.Owner.Phone,
                                Address = p.Owner.Address,
                                Pets = null
                            },
                            Schedulings = p.Schedulings.Select(p => new SchedulingModel
                            {
                                Id = p.Id,
                                Date = p.Date,
                                Time = p.Time,
                                PetId = p.PetId,
                                ServiceId = p.ServiceId,
                                Status = p.Status,
                                Pet = null,
                                Service = null
                            }).ToList()
                        })
                        .FirstOrDefaultAsync(x => x.Id == petId);
        return pet;
    }

    public async Task<IEnumerable<PetModel>> GetPetsAsync()
    {
        return await _context.Pets.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<PetModel>> GetPetsAsync(int ownerId)
    {
        return await _context.Pets.AsNoTracking().Where(x => x.OwnerId == ownerId).ToListAsync();
    }

    public async Task<PetModel> InsertPetAsync(PetModel petModel)
    {
        await _context.Pets.AddAsync(petModel);
        await _context.SaveChangesAsync();

        return petModel;
    }

    public async Task<PetModel> UpdatePetAsync(int petId, PetModel petModel, int ownerId)
    {
        await _context
       .Pets
       .Where(t => t.Id == petId && t.OwnerId == ownerId)
       .ExecuteUpdateAsync(setters => setters
            .SetProperty(e => e.Name, petModel.Name)
            .SetProperty(e => e.Size, petModel.Size)
            .SetProperty(e => e.Race, petModel.Race)
            .SetProperty(e => e.OwnerId, petModel.OwnerId)
        );

        await _context.SaveChangesAsync();

        return petModel;
    }
}