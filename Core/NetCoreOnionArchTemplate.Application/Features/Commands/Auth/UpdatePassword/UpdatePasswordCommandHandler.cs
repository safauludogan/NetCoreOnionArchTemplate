﻿using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.Exceptions;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.UpdatePassword
{
	public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
	{
		private readonly IUserService _userService;

		public UpdatePasswordCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
		{
			if (!request.Password.Equals(request.PasswordConfirm))
				throw new PasswordChangeException("Şifreler uyuşmuyor");

            var result = await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
            return new UpdatePasswordCommandResponse { IsSuccess = result };
        }
	}
}
