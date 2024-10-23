using PetShop.Modules.Pets;
using PetShop.Modules.Pets.Interfaces;
using PetShop.Modules.Scheduling.Interfaces;
using PetShop.Response;

namespace PetShop.Modules.Scheduling;

public class SchedulingService : ISchedulingService
{
    private readonly ISchedulingRepository _schedulingRepository;
    private readonly IPetRepository _petRepository;

    public SchedulingService(ISchedulingRepository schedulingRepository, IPetRepository petRepository)
    {
        _schedulingRepository = schedulingRepository;
        _petRepository = petRepository;
    }

    public async Task DeleteSchedulingAsync(int schedulingId, int ownerId = 0)
    {
        if (ownerId == 0)
            await _schedulingRepository.DeleteSchedulingAsync(schedulingId);
        else
            await _schedulingRepository.DeleteSchedulingAsync(schedulingId, ownerId);
    }

    public async Task<Either<Exception, SchedulingDTO>> GetSchedulingByIdAsync(int schedulingId, int ownerId = 0)
    {
        try
        {
            SchedulingModel response;

            if (ownerId == 0)
                response = await _schedulingRepository.GetSchedulingByIdAsync(schedulingId);
            else
                response = await _schedulingRepository.GetSchedulingByIdAsync(schedulingId, ownerId);

            return response?.ToDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, List<SchedulingDTO>>> GetSchedulingsAsync(int ownerId = 0)
    {
        try
        {
            IEnumerable<SchedulingModel> response;

            if (ownerId == 0)
                response = await _schedulingRepository.GetSchedulingsAsync();
            else
                response = await _schedulingRepository.GetSchedulingsAsync(ownerId);

            var responseDTO = response?.Select(x => x.ToDTO()).ToList();
            return responseDTO;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, SchedulingDTO>> InsertSchedulingAsync(SchedulingDTO schedulingDTO, int ownerId = 0)
    {
        try
        {
            var schedulingModel = schedulingDTO.ToModel();

            PetModel? pet;

            if (ownerId == 0)
                pet = await _petRepository.GetPetByIdAsync(schedulingModel.PetId);
            else
                pet = await _petRepository.GetPetByIdAsync(schedulingModel.PetId, ownerId);

            if (pet is null)
                return null;

            await _schedulingRepository.InsertSchedulingAsync(schedulingModel);
            return schedulingModel?.ToDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, SchedulingDTO>> UpdateSchedulingAsync(int schedulingId, SchedulingDTO schedulingDTO, int ownerId)
    {
        try
        {
            var schedulingModel = schedulingDTO.ToModel();
            schedulingModel.Id = schedulingId;

            if (ownerId == 0)
                await _schedulingRepository.UpdateSchedulingAsync(schedulingId, schedulingModel);
            else
                await _schedulingRepository.UpdateSchedulingAsync(schedulingId, schedulingModel, ownerId);

            return schedulingModel?.ToDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}

