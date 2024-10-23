namespace PetShop.Modules.Services.Interfaces;

public interface IServiceRepository
{
    public Task<IEnumerable<ServiceModel>> GetServicesAsync();
    public Task<IServiceModel> GetServiceByIdAsync(int serviceId);
    public Task<IServiceModel> InsertServiceAsync(ServiceModel serviceModel);
    public Task<IServiceModel> UpdateServiceAsync(int serviceId, ServiceModel serviceModel);
    public Task DeleteServiceAsync(int serviceId);
}
