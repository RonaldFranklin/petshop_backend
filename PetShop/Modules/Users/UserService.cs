using PetShop.Modules.Users.Interfaces;
using PetShop.Response;

namespace PetShop.Modules.Users;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<Either<Exception, UserDTO>> GetUserByIdAsync(int userId)
    {
        try
        {
            var response = await _userRepository.GetUserByIdAsync(userId);
            return response?.ToDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, List<UserDTO>>> GetUsersAsync(int userToSearchId = 0)
    {
        try
        {
            IEnumerable<UserModel> response;

            if(userToSearchId == 0)
                response = await _userRepository.GetUsersAsync();
            else
                response = await _userRepository.GetUsersAsync(userToSearchId);

            var responseDTO = response?.Select(x => x.ToDTO()).ToList();
            return responseDTO;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, UserDTO>> InsertUserAsync(UserDTO userDTO)
    {
        try
        {
            var serviceModel = userDTO.ToModel();
            await _userRepository.InsertUserAsync(serviceModel);
            return serviceModel?.ToDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, string>> Login(string email, string password)
    {
        try
        {
            var token = await _userRepository.Login(email, password);
            return token;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, UserDTO>> UpdateUserAsync(int userId, UserDTO userDTO)
    {
        try
        {
            var userModel = userDTO.ToModel();
            userModel.Id = userId;
            await _userRepository.UpdateUserAsync(userId, userModel);
            return userModel?.ToDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
