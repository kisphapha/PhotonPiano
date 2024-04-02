using AutoMapper;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Helper.Exceptions;

namespace PhotonPiano.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<GetUserDto>> GetUsers()
        {
            return (await _userRepository.GetAllAsync()).Adapt<List<GetUserDto>>();
        }
        public async Task<GetUserDto> GetUserById(long id)
        {
            return (await _userRepository.GetByIdAsync(id)).Adapt<GetUserDto>();
        }
        public async Task<GetLoginedUserDto> GetUserWithStudentsAndInstructors(long id)
        {
            return (await _userRepository.GetUserWithStudentsAndInstructorsByIdAsync(id)).Adapt<GetLoginedUserDto>();
        }
        public async Task<GetUserDto> VerifyLogin(string? emailOrPhone, string? password)
        {
            if (emailOrPhone.IsNullOrEmpty())
            {
                throw new BadRequestException("Input Email or phone number is empty");
            }
            if (password.IsNullOrEmpty())
            {
                throw new BadRequestException("Input password is empty");
            }
            var user = await _userRepository.FindOneAsync(u => (u.Email == emailOrPhone || u.Phone == emailOrPhone) && u.Password!.Equals(password));

            if (user is null)
            {
                throw new NotFoundException("Wrong email, phone number or password");
            }

            return user.Adapt<GetUserDto>();
        }
    }
}
