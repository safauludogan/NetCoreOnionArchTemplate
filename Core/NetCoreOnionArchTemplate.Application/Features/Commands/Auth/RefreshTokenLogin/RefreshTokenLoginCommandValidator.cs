using FluentValidation;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandValidator : AbstractValidator<RefreshTokenLoginCommandRequest>
    {
        public RefreshTokenLoginCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }
}
