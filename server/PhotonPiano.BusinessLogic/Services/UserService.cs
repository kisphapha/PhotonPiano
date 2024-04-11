using AutoMapper;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentService _studentService;

        public UserService(IUserRepository userRepository, IStudentService studentService)
        {
            _userRepository = userRepository;
            _studentService = studentService;
        }

        public async Task<List<GetUserDto>> GetUsers()
        {
            return (await _userRepository.GetAllAsync()).Adapt<List<GetUserDto>>();
        }
        public async Task<User?> GetUserById(long id)
        {
            return await _userRepository.GetByIdAsync(id);
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

        public async Task<GetUserWithStudentDto> CreateUser(CreateUserDto createUserDto)
        {
            var existedUser = await _userRepository.FindOneAsync(u => u.Email == createUserDto.Email || u.Phone == createUserDto.Phone);

            if (existedUser is not null)
            {
                throw new BadRequestException("Email or phone number is already existed");
            }

            var mappedUser = createUserDto.Adapt<User>();
            //mappedUser.DoB = (createUserDto.CreateDob is not null) ? 
            //    DateOnly.FromDateTime(createUserDto.CreateDob.Value) : DateOnly.MinValue;
            mappedUser.Role = Role.Student.ToString();
            mappedUser.Picture = "src/assets/noavatar.jpg";

            var user = await _userRepository.AddAsync(mappedUser);

            var student = await _studentService.CreateStudentAfterCreatedUser(user.Id);

            var getUserWithStudentDto = user.Adapt<GetUserWithStudentDto>();
            getUserWithStudentDto.Student = student;

            return getUserWithStudentDto;
        }

        public async Task UpdateUser(long userId, UpdateUserDto updateUserDto)
        {
            var curentUser = await GetUserById(userId);
            if (curentUser is null)
            {
                throw new NotFoundException("User not found");
            }
            if (!string.IsNullOrEmpty(updateUserDto.Name))
            {
                curentUser.Name = updateUserDto.Name;
            }
            if (updateUserDto.DoB is not null)
            {
                curentUser.DoB = updateUserDto.DoB;
            }
            curentUser.Address = updateUserDto.Address;
            curentUser.Gender = updateUserDto.Gender;
            curentUser.BankAccount = updateUserDto.BankAccount;
            await _userRepository.UpdateAsync(curentUser);
        }

    }
}
