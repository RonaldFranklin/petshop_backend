using FluentValidation;

namespace PetShop.Modules.Pets.Validator;

public sealed class PetDTOValidator : AbstractValidator<PetDTO>
{
    public PetDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O 'Name' obrigatório.")
            .Length(2, 255).WithMessage("O 'Name' deve ser maior do que 2 e menor do que 255.");

        RuleFor(x => x.Size)
           .NotEmpty().WithMessage("O 'Size' obrigatório.")
           .Length(2, 50).WithMessage("O 'Size' deve ser maior do que 2 e menor do que 50.");

        RuleFor(x => x.Race)
           .NotEmpty().WithMessage("O 'Race' obrigatório.")
           .Length(2, 255).WithMessage("O 'Race' deve ser maior do que 2 e menor do que 255.");

        RuleFor(x => x.OwnerId)
           .NotEmpty().WithMessage("O 'OwnerId' obrigatório.")
           .GreaterThan(0).WithMessage("O 'OwnerId' deve ser maior do que 0.");
    }
}
