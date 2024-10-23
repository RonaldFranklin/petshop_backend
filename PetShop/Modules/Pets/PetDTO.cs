using PetShop.Modules.Scheduling;
using PetShop.Modules.Users;
using System.Text.Json.Serialization;

namespace PetShop.Modules.Pets;

public class PetDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public int OwnerId { get; set; }

    [JsonIgnore]
    public UserDTO Owner { get; set; }

    [JsonPropertyName("Owner")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserDTO GetOwner => Owner;

    [JsonIgnore]
    public ICollection<SchedulingDTO> Schedulings { get; set; } = new List<SchedulingDTO>();

    [JsonPropertyName("Schedulings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<SchedulingDTO> GetSchedulings => Schedulings;

    public PetModel ToPetModel()
    {
        var petModel = new PetModel
        {
            Name = Name,
            Race = Race,
            Size = Size,
            OwnerId = OwnerId
        };

        return petModel;
    }

    public void SetToModelId(int modelId)
    {
        Id = modelId;
    }

    public void SetToModelOwner(UserModel owner)
    {
        Owner = owner?.ToDTO();
    }

    public void SetToModelSchedulings(ICollection<SchedulingModel> schedulings)
    {
        if (Schedulings is not null)
            Schedulings = schedulings?.Select(x => x.ToDTO()).ToList();
    }
}
