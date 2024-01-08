﻿using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.AppUser.VerifyResetToken
{
	public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
	{
        public string ResetToken { get; set; }
        public string UserId { get; set; }
    }
}