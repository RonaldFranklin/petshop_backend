using PetShop.Modules.Scheduling;
using PetShop.Modules.Services.Interfaces;

namespace PetShop.Modules.Services;

public class ServiceModel : IServiceModel
{
    public int Id { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ICollection<SchedulingModel> Schedulings { get; set; }

    public ServiceDTO ToServiceDTO()
    {
        var serviceDto = new ServiceDTO
        {
            ServiceType = ServiceType,
            Price = Price
        };

        serviceDto.SetToModelId(Id);

        return serviceDto;
    }
}
