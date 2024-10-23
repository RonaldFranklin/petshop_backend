namespace PetShop.Modules.Users.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<UserModel>> GetUsersAsync();
    public Task<IEnumerable<UserModel>> GetUsersAsync(int userToSearchId);
    public Task<UserModel> GetUserByIdAsync(int userId);
    public Task<UserModel> InsertUserAsync(UserModel userModel);
    public Task<UserModel> UpdateUserAsync(int userId, UserModel userModel);
    public Task DeleteUserAsync(int userId);
    public Task<string> Login(string email, string password);
}
