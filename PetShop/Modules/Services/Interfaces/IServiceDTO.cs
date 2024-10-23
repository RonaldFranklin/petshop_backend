namespace PetShop.Modules.Services.Interfaces;

public interface IServiceDTO
{
    public ServiceModel ToServiceModel();

    public void SetToModelId(int modelId);
}
