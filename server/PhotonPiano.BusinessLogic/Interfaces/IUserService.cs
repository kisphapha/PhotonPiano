﻿using PhotonPiano.Helper.Dtos.Users;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUserDto>> GetUsers();

        Task<GetUserDto> GetUserById(long id);
        Task<GetUserDto> VerifyLogin(string? emailOrPhone, string? password);
    }
}
