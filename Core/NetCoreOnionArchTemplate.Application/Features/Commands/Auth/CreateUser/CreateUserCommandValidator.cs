
using FluentValidation;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.NameSurname)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2)
                .WithName("İsim Soyisim");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(60)
                .EmailAddress()
                .MinimumLength(8)
                .WithName("E-posta adresi");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Parola");

            RuleFor(x => x.PasswordConfirm)
                .NotEmpty()
                .MinimumLength(6)
                .Equal(x => x.Password)
                .WithName("Parola Tekrarı");
        }
    }
}
