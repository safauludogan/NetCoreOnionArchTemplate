using NetCoreOnionArchTemplate.Application.Base;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Exceptions;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Rules
{
    public class AuthRules : BaseRules
    {
        public Task UserShouldNotBeExists(AppUser? user)
        {
            if (user is not null) throw new UserAlreadyExistsException();
            return Task.CompletedTask;
        }

        public Task EmailAndUsernameOrPasswordShouldNotBeInvalid(AppUser? user, bool checkPassword)
        {
            if (user is null || !checkPassword) throw new EmailAndUsernameOrPasswordShouldNotBeInvalidException();
            return Task.CompletedTask;
        }

        public Task EmailAddressShouldNotBeInvalid(AppUser? user)
        {
            if (user is null) throw new EmailAddressShouldNotBeInvalidException();
            return Task.CompletedTask;
        }
    }
}
