﻿using Microsoft.AspNetCore.Identity;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.DTOs.User;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = model.UserName,
                NameSurname = model.NameSurname,
                Email = model.Email,
            }, model.Password);

            CreateUserResponse response = new() { IsSuccess = result.Succeeded };
            if (result.Succeeded)
                response.Message = "User Created";
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code} - {error.Description}\n";
            }
            return response;
        }
    }
}
