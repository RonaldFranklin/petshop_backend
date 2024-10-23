using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Modules.Services.Interfaces;

namespace PetShop.Modules.Services;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteServiceAsync(int serviceId)
    {
        await _context
            .Services
            .Where(t => t.Id == serviceId)
            .ExecuteDeleteAsync();

        await _context.SaveChangesAsync();
    }

    public async Task<IServiceModel> GetServiceByIdAsync(int serviceId)
    {
        var service = await _context
                            .Services
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == serviceId);

        return service;
    }

    public async Task<IEnumerable<ServiceModel>> GetServicesAsync()
    {
        return await _context.Services.AsNoTracking().ToListAsync();
    }

    public async Task<IServiceModel> InsertServiceAsync(ServiceModel serviceModel)
    {
        await _context.Services.AddAsync(serviceModel);
        await _context.SaveChangesAsync();

        return serviceModel;
    }

    public async Task<IServiceModel> UpdateServiceAsync(int serviceId, ServiceModel serviceModel)
    {
        await _context
           .Services
           .Where(t => t.Id == serviceId)
           .ExecuteUpdateAsync( setters => setters
                .SetProperty(e => e.ServiceType, serviceModel.ServiceType)
                .SetProperty(e => e.Price, serviceModel.Price)
            );

        await _context.SaveChangesAsync();

        return serviceModel;
    }
}
