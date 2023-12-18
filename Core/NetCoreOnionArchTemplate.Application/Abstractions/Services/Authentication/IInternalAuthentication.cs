﻿namespace NetCoreOnionArchTemplate.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<DTOs.Token> LoginAsync(string usernameOrEmail, string password,int accessTokenLifeTime);
    }
}
