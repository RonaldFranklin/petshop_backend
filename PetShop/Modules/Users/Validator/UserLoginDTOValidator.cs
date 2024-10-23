using FluentValidation;

namespace PetShop.Modules.Users.Validator;

public class UserLoginDTOValidator : AbstractValidator<UserLoginDTO>
{
    public UserLoginDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O 'Email' obrigatório.");

        RuleFor(x => x.Password)
           .NotEmpty().WithMessage("O 'Password' obrigatório.");
    }
}
