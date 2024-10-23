namespace PetShop.Modules.Pets.Interfaces;

public interface IPetRepository
{
    public Task<IEnumerable<PetModel>> GetPetsAsync();
    public Task<IEnumerable<PetModel>> GetPetsAsync(int ownerId);
    public Task<PetModel> GetPetByIdAsync(int petId);
    public Task<PetModel> GetPetByIdAsync(int petId, int ownerId);
    public Task<PetModel> InsertPetAsync(PetModel petModel);
    public Task<PetModel> UpdatePetAsync(int petId, PetModel petModel, int ownerId);
    public Task DeletePetAsync(int petId, int ownerId);
}
