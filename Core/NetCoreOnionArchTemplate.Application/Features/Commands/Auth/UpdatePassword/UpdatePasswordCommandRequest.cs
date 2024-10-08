﻿using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.UpdatePassword
{
	public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
	{
        public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string  Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}