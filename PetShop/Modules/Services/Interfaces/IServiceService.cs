using PetShop.Response;

namespace PetShop.Modules.Services.Interfaces;

public interface IServiceService
{
    public Task<Either<Exception,List<ServiceDTO>>> GetServicesAsync();
    public Task<Either<Exception,ServiceDTO>> GetServiceByIdAsync(int serviceId);
    public Task<Either<Exception,ServiceDTO>> InsertServiceAsync(ServiceDTO serviceDTO);
    public Task<Either<Exception,ServiceDTO>> UpdateServiceAsync(int serviceId, ServiceDTO serviceDTO);
    public Task DeleteServiceAsync(int serviceId);
}
