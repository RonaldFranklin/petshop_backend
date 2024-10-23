using PetShop.Modules.Services.Interfaces;
using PetShop.Response;

namespace PetShop.Modules.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository) 
    {
        _serviceRepository = serviceRepository;
    }

    public async Task DeleteServiceAsync(int serviceId)
    {
        await _serviceRepository.DeleteServiceAsync(serviceId);
    }

    public async Task<Either<Exception, ServiceDTO>> GetServiceByIdAsync(int serviceId)
    {
        try
        {
            var response = await _serviceRepository.GetServiceByIdAsync(serviceId);
            return response?.ToServiceDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, List<ServiceDTO>>> GetServicesAsync()
    {
        try
        {
            var response = await _serviceRepository.GetServicesAsync();
            var responseDTO = response?.Select(x => x.ToServiceDTO()).ToList();
            return responseDTO;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, ServiceDTO>> UpdateServiceAsync(int serviceId, ServiceDTO serviceDTO)
    {
        try
        {
            var serviceModel = serviceDTO.ToServiceModel();
            serviceModel.Id = serviceId;
            await _serviceRepository.UpdateServiceAsync(serviceId, serviceModel);
            return serviceModel?.ToServiceDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, ServiceDTO>> InsertServiceAsync(ServiceDTO serviceDTO)
    {
        try
        {
            var serviceModel = serviceDTO.ToServiceModel();
            await _serviceRepository.InsertServiceAsync(serviceModel);
            return serviceModel?.ToServiceDTO(); 
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
