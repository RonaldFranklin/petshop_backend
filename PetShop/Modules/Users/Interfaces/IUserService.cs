using PetShop.Response;

namespace PetShop.Modules.Users.Interfaces;

public interface IUserService
{
    public Task<Either<Exception, string>> Login(string email, string password);
    public Task<Either<Exception, List<UserDTO>>> GetUsersAsync(int userToSearchId = 0);
    public Task<Either<Exception, UserDTO>> GetUserByIdAsync(int userId);
    public Task<Either<Exception, UserDTO>> InsertUserAsync(UserDTO userDTO);
    public Task<Either<Exception, UserDTO>> UpdateUserAsync(int userId, UserDTO userDTO);
    public Task DeleteUserAsync(int userId);
}
