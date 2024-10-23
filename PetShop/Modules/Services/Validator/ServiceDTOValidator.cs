using FluentValidation;

namespace PetShop.Modules.Services.Validator;

public sealed class ServiceDTOValidator : AbstractValidator<ServiceDTO>
{
    public ServiceDTOValidator()
    {
        RuleFor(x => x.ServiceType)
            .NotEmpty().WithMessage("O 'ServiceType' obrigatório.")
            .Length(2,250).WithMessage("O 'ServiceType' deve ser maior do que 2 e menor do que 250.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("O 'Price' obrigatório.")
            .GreaterThan(0).WithMessage("O 'Price' deve ser maior do que 0.");
    }
}