using PetShop.Modules.Pets;
using System.Text.Json.Serialization;

namespace PetShop.Modules.Users;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<PetDTO> Pets { get; set; } = null;

    [JsonPropertyName("Pets")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<PetDTO> GetPets => Pets;


    public UserModel ToModel()
    {
        var petModel = new UserModel
        {
            Name = Name,
            Email = Email,
            Phone = Phone,
            Address = Address,
            Password = Password
        };

        return petModel;
    }

    public void SetToModelId(int modelId)
    {
        Id = modelId;
    }

    public void SetToModelPets(ICollection<PetModel> pets)
    {
        if (pets is not null)
            Pets = pets.Select(x => x.ToPetDTO()).ToList();
    }
}
