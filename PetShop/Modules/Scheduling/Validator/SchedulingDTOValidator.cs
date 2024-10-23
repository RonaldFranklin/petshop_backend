using FluentValidation;

namespace PetShop.Modules.Scheduling.Validator;

public class SchedulingDTOValidator : AbstractValidator<SchedulingDTO>
{
    public SchedulingDTOValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("O 'Date' obrigatório.");

        RuleFor(x => x.Time)
           .NotEmpty().WithMessage("O 'Time' obrigatório.");

        RuleFor(x => x.Status)
           .Length(2, 50).WithMessage("O 'Status' deve ser maior do que 2 e menor do que 50.");

        RuleFor(x => x.PetId)
           .NotEmpty().WithMessage("O 'PetId' obrigatório.")
           .GreaterThan(0).WithMessage("O 'PetId' deve ser maior do que 0.");

        RuleFor(x => x.ServiceId)
           .NotEmpty().WithMessage("O 'ServiceId' obrigatório.")
           .GreaterThan(0).WithMessage("O 'ServiceId' deve ser maior do que 0.");
    }
}