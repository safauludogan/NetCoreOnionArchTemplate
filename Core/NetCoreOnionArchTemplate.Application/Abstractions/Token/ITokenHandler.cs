﻿namespace NetCoreOnionArchTemplate.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(int time);
        string CreateRefreshToken();
    }
}