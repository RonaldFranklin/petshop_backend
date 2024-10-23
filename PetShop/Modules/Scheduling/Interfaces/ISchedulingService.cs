using PetShop.Response;

namespace PetShop.Modules.Scheduling.Interfaces;

public interface ISchedulingService
{
    public Task<Either<Exception, List<SchedulingDTO>>> GetSchedulingsAsync(int ownerId = 0);
    public Task<Either<Exception, SchedulingDTO>> GetSchedulingByIdAsync(int schedulingId, int ownerId = 0);
    public Task<Either<Exception, SchedulingDTO>> InsertSchedulingAsync(SchedulingDTO schedulingDTO, int ownerId = 0);
    public Task<Either<Exception, SchedulingDTO>> UpdateSchedulingAsync(int schedulingId, SchedulingDTO schedulingDTO, int ownerId);
    public Task DeleteSchedulingAsync(int schedulingId, int ownerId = 0);
}
