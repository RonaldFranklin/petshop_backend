using FluentValidation;

namespace PetShop.Modules.Users.Validator;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O 'Name' obrigatório.")
            .Length(2, 250).WithMessage("O 'Name' deve ser maior do que 2 e menor do que 250.");

        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("O 'Email' obrigatório.")
           .Length(2, 250).WithMessage("O 'Email' deve ser maior do que 2 e menor do que 250.");

        RuleFor(x => x.Password)
           .NotEmpty().WithMessage("O 'Password' obrigatório.")
           .Length(8, 16).WithMessage("O 'Password' deve ser maior do que 8 e menor do que 16.");

        RuleFor(x => x.Address)
           .Length(2, 250).WithMessage("O 'Address' deve ser maior do que 2 e menor do que 250.");

        RuleFor(x => x.Phone)
          .Length(2, 250).WithMessage("O 'Phone' deve ser maior do que 2 e menor do que 250.");
    }
}
