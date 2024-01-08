﻿using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.AppUser.PasswordReset
{
	public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
	{
        public string Email { get; set; }
    }
}