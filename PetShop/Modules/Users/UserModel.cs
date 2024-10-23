using PetShop.Modules.Pets;

namespace PetShop.Modules.Users;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public ICollection<PetModel> Pets { get; set; }

    public UserDTO ToDTO()
    {
        var userDto = new UserDTO
        {
            Name = Name,
            Email = Email,
            Phone = Phone,
            Address = Address,
        };

        userDto.SetToModelId(Id);
        userDto.SetToModelPets(Pets);

        return userDto;
    }
}
