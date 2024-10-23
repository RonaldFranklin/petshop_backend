using PetShop.Modules.Scheduling;
using PetShop.Modules.Users;

namespace PetShop.Modules.Pets;

public class PetModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;

    public int OwnerId { get; set; }
    public UserModel Owner { get; set; }
    public ICollection<SchedulingModel> Schedulings { get; set; }

    public PetDTO ToPetDTO()
    {
        var petDto = new PetDTO
        {
           Id = Id,
           Name = Name,
           OwnerId = OwnerId,
           Size = Size,
           Race = Race
        };

        petDto.SetToModelId(Id);
        petDto.SetToModelOwner(Owner);
        petDto.SetToModelSchedulings(Schedulings);

        return petDto;
    }
}
