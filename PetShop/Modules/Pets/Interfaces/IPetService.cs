using PetShop.Response;

namespace PetShop.Modules.Pets.Interfaces;

public interface IPetService
{
    public Task<Either<Exception, List<PetDTO>>> GetPetsAsync(int ownerId = 0);
    public Task<Either<Exception, PetDTO>> GetPetByIdAsync(int petId, int ownerId = 0);
    public Task<Either<Exception, PetDTO>> InsertPetAsync(PetDTO petDTO);
    public Task<Either<Exception, PetDTO>> UpdatePetAsync(int petId, PetDTO petDTO, int ownerId);
    public Task DeletePetAsync(int petId, int ownerId);
}
