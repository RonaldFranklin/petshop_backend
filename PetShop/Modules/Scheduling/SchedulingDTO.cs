using PetShop.Modules.Pets;
using PetShop.Modules.Services;
using System.Text.Json.Serialization;

namespace PetShop.Modules.Scheduling;

public class SchedulingDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public int PetId { get; set; }

    [JsonIgnore]
    public PetDTO Pet { get; set; }

    [JsonPropertyName("Pet")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PetDTO GetPet => Pet;

    public int ServiceId { get; set; }

    [JsonIgnore]
    public ServiceDTO Service { get; set; }

    [JsonPropertyName("Service")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ServiceDTO GetService => Service;

    public string Status { get; set; } = string.Empty;

    public SchedulingModel ToModel()
    {
        var model = new SchedulingModel 
        { 
            Date = Date,
            Time = Time,
            PetId = PetId,
            ServiceId = ServiceId,
            Status = Status
        };

        return model;
    }

    public void SetToPetModel(PetModel pet)
    {
        Pet = pet?.ToPetDTO();
    }

    public void SetToServiceModel(ServiceModel service)
    {
        Service = service?.ToServiceDTO();
    }
}
