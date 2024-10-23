using PetShop.Modules.Pets.Interfaces;
using PetShop.Response;

namespace PetShop.Modules.Pets;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;

    public PetService(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task DeletePetAsync(int petId, int ownerId)
    {
        await _petRepository.DeletePetAsync(petId, ownerId);
    }

    public async Task<Either<Exception, PetDTO>> GetPetByIdAsync(int petId, int ownerId = 0)
    {
        try
        {
            PetModel response;

            if(ownerId == 0)
                response = await _petRepository.GetPetByIdAsync(petId);
            else
                response = await _petRepository.GetPetByIdAsync(petId, ownerId);

            return response?.ToPetDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, List<PetDTO>>> GetPetsAsync(int ownerId = 0)
    {
        try
        {
            IEnumerable<PetModel> response;

            if(ownerId == 0)
                response = await _petRepository.GetPetsAsync();
            else
                response = await _petRepository.GetPetsAsync(ownerId);

            var responseDTO = response?.Select(x => x.ToPetDTO()).ToList();
            return responseDTO;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, PetDTO>> InsertPetAsync(PetDTO petDTO)
    {
        try
        {
            var serviceModel = petDTO.ToPetModel();
            await _petRepository.InsertPetAsync(serviceModel);
            return serviceModel?.ToPetDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Either<Exception, PetDTO>> UpdatePetAsync(int petId, PetDTO petDTO, int ownerId)
    {
        try
        {
            var petModel = petDTO.ToPetModel();
            petModel.Id = petId;
            await _petRepository.UpdatePetAsync(petId, petModel, ownerId);
            return petModel?.ToPetDTO();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
