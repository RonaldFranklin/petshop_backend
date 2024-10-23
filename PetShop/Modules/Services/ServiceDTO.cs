using PetShop.Modules.Services.Interfaces;

namespace PetShop.Modules.Services;

public class ServiceDTO : IServiceDTO
{
    public int Id { get; private set; }
    public string ServiceType { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public ServiceModel ToServiceModel()
    {
        return new ServiceModel 
        { 
            ServiceType = ServiceType, 
            Price = Price 
        };
    }

    public void SetToModelId(int modelId)
    {
        Id = modelId;
    }
}