namespace PetShop.Modules.Scheduling.Interfaces;

public interface ISchedulingRepository
{
    public Task<IEnumerable<SchedulingModel>> GetSchedulingsAsync();
    public Task<IEnumerable<SchedulingModel>> GetSchedulingsAsync(int ownerId);
    public Task<SchedulingModel> GetSchedulingByIdAsync(int schedulingId);
    public Task<SchedulingModel> GetSchedulingByIdAsync(int schedulingId, int ownerId);
    public Task<SchedulingModel> InsertSchedulingAsync(SchedulingModel schedulingModel);
    public Task<SchedulingModel> UpdateSchedulingAsync(int schedulingId, SchedulingModel schedulingModel);
    public Task<SchedulingModel> UpdateSchedulingAsync(int schedulingId, SchedulingModel schedulingModel, int ownerId);
    public Task DeleteSchedulingAsync(int schedulingId);
    public Task DeleteSchedulingAsync(int schedulingId, int ownerId);
}
