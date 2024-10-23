using PetShop.Modules.Pets;
using PetShop.Modules.Services;

namespace PetShop.Modules.Scheduling;

public class SchedulingModel
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public int PetId { get; set; }
    public PetModel Pet { get; set; }
    public int ServiceId { get; set; }
    public ServiceModel Service { get; set; }
    public string Status { get; set; } = string.Empty;

    public SchedulingDTO ToDTO()
    {
        var schedulingDTO = new SchedulingDTO
        {
            Id = Id,
            Date = Date,
            Time = Time,
            PetId = PetId,
            ServiceId = ServiceId,
            Status = Status
        };

        schedulingDTO.SetToPetModel(Pet);
        schedulingDTO.SetToServiceModel(Service);

        return schedulingDTO;
    }
}
