using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Modules.Pets;
using PetShop.Modules.Scheduling.Interfaces;
using PetShop.Modules.Services;
using PetShop.Modules.Users;

namespace PetShop.Modules.Scheduling;

public class SchedulingRepository : ISchedulingRepository
{
    private readonly AppDbContext _context;

    public SchedulingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteSchedulingAsync(int schedulingId)
    {
        await _context
        .Schedulings
        .Where(t => t.Id == schedulingId)
        .ExecuteDeleteAsync();

        await _context.SaveChangesAsync();
    }

    public async Task DeleteSchedulingAsync(int schedulingId, int ownerId)
    {
        await _context
        .Schedulings
        .Where(t => t.Id == schedulingId && t.Pet.OwnerId == ownerId)
        .ExecuteDeleteAsync();

        await _context.SaveChangesAsync();
    }

    public async Task<SchedulingModel> GetSchedulingByIdAsync(int schedulingId, int ownerId)
    {
        var scheduling = await _context
                     .Schedulings
                     .AsNoTracking()
                     .Where(x => x.Pet.OwnerId == ownerId)
                     .Select(p => new SchedulingModel
                     {
                         Id = p.Id,
                         Date = p.Date,
                         Time = p.Time,
                         PetId = p.PetId,
                         Status = p.Status,
                         ServiceId = p.ServiceId,
                         Pet = new PetModel
                         {
                             Id = p.Pet.Id,
                             Name = p.Pet.Name,
                             Race = p.Pet.Race,
                             Size = p.Pet.Size,
                             OwnerId = p.Pet.OwnerId,
                             Owner = new UserModel()
                             {
                                 Name = p.Pet.Owner.Name,
                                 Phone = p.Pet.Owner.Phone,
                                 Email = p.Pet.Owner.Email,
                                 Address = p.Pet.Owner.Address
                             },
                             Schedulings = null
                         },
                         Service = new ServiceModel
                         {
                             Id = p.Service.Id,
                             ServiceType = p.Service.ServiceType,
                             Price = p.Service.Price,
                             Schedulings = null
                         }
                     })
                     .FirstOrDefaultAsync(x => x.Id == schedulingId);
        return scheduling;
    }

    public async Task<SchedulingModel> GetSchedulingByIdAsync(int schedulingId)
    {
        var scheduling = await _context
                    .Schedulings
                    .AsNoTracking()
                    .Select(p => new SchedulingModel
                    {
                        Id = p.Id,
                        Date = p.Date,
                        Time = p.Time,
                        PetId = p.PetId,
                        Status = p.Status,
                        ServiceId = p.ServiceId,
                        Pet = new PetModel
                        {
                            Id = p.Pet.Id,
                            Name = p.Pet.Name,
                            Race = p.Pet.Race,
                            Size = p.Pet.Size,
                            OwnerId = p.Pet.OwnerId,
                            Owner = new UserModel()
                            {
                                Name = p.Pet.Owner.Name,
                                Phone = p.Pet.Owner.Phone,
                                Email = p.Pet.Owner.Email,
                                Address = p.Pet.Owner.Address
                            },
                            Schedulings = null
                        },
                        Service = new ServiceModel
                        {
                            Id = p.Service.Id,
                            ServiceType = p.Service.ServiceType,
                            Price = p.Service.Price,
                            Schedulings = null
                        }
                    })
                    .FirstOrDefaultAsync(x => x.Id == schedulingId);
        return scheduling;
    }

    public async Task<IEnumerable<SchedulingModel>> GetSchedulingsAsync()
    {
        return await _context.Schedulings.AsNoTracking().Select(p => new SchedulingModel
        {
            Id = p.Id,
            Date = p.Date,
            Time = p.Time,
            PetId = p.PetId,
            Status = p.Status,
            ServiceId = p.ServiceId,
            Pet = new PetModel
            {
                Id = p.Pet.Id,
                Name = p.Pet.Name,
                Race = p.Pet.Race,
                Size = p.Pet.Size,
                OwnerId = p.Pet.OwnerId,
                Owner = new UserModel()
                {
                    Name = p.Pet.Owner.Name,
                    Phone = p.Pet.Owner.Phone,
                    Email = p.Pet.Owner.Email,
                    Address = p.Pet.Owner.Address
                },
                Schedulings = null
            },
            Service = new ServiceModel
            {
                Id = p.Service.Id,
                ServiceType = p.Service.ServiceType,
                Price = p.Service.Price,
                Schedulings = null
            }
        }).ToListAsync();
    }

    public async Task<IEnumerable<SchedulingModel>> GetSchedulingsAsync(int ownerId)
    {
        return await _context.Schedulings.AsNoTracking().Where(x => x.Pet.OwnerId == ownerId).Select(p => new SchedulingModel
        {
            Id = p.Id,
            Date = p.Date,
            Time = p.Time,
            PetId = p.PetId,
            Status = p.Status,
            ServiceId = p.ServiceId,
            Pet = new PetModel
            {
                Id = p.Pet.Id,
                Name = p.Pet.Name,
                Race = p.Pet.Race,
                Size = p.Pet.Size,
                OwnerId = p.Pet.OwnerId,
                Owner = new UserModel()
                {
                    Name = p.Pet.Owner.Name,
                    Phone = p.Pet.Owner.Phone,
                    Email = p.Pet.Owner.Email,
                    Address = p.Pet.Owner.Address
                },
                Schedulings = null
            },
            Service = new ServiceModel
            {
                Id = p.Service.Id,
                ServiceType = p.Service.ServiceType,
                Price = p.Service.Price,
                Schedulings = null
            }
        }).ToListAsync();
    }

    public async Task<SchedulingModel> InsertSchedulingAsync(SchedulingModel schedulingModel)
    {
        await _context.Schedulings.AddAsync(schedulingModel);
        await _context.SaveChangesAsync();

        return schedulingModel;
    }

    public async Task<SchedulingModel> UpdateSchedulingAsync(int schedulingId, SchedulingModel schedulingModel)
    {
        await _context
       .Schedulings
       .Where(t => t.Id == schedulingId)
       .ExecuteUpdateAsync(setters => setters
            .SetProperty(e => e.Date, schedulingModel.Date)
            .SetProperty(e => e.Time, schedulingModel.Time)
            .SetProperty(e => e.Status, schedulingModel.Status)
            .SetProperty(e => e.ServiceId, schedulingModel.ServiceId)
        );

        await _context.SaveChangesAsync();

        return schedulingModel;
    }

    public async Task<SchedulingModel> UpdateSchedulingAsync(int schedulingId, SchedulingModel schedulingModel, int ownerId)
    {
        await _context
       .Schedulings
       .Where(t => t.Id == schedulingId && t.Pet.OwnerId == ownerId)
       .ExecuteUpdateAsync(setters => setters
            .SetProperty(e => e.Date, schedulingModel.Date)
            .SetProperty(e => e.Time, schedulingModel.Time)
            .SetProperty(e => e.Status, schedulingModel.Status)
            .SetProperty(e => e.ServiceId, schedulingModel.ServiceId)
        );

        await _context.SaveChangesAsync();

        return schedulingModel;
    }
}