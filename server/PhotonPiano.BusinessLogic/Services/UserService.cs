using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.User;
using PhotonPiano.Helper.Exceptions;

namespace PhotonPiano.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUserDto>> GetUsers()
        {
            return _mapper.Map<List<GetUserDto>>(await _userRepository.GetAllAsync());
        }
        public async Task<GetUserDto> GetUserById(long id)
        {
            return _mapper.Map<GetUserDto>(await _userRepository.GetByIdAsync(id));
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
            var user = await _userRepository.FindOneAsync(u => (u.Email == emailOrPhone || u.Phone == emailOrPhone) && u.Password == password);

            if (user is null)
            {
                throw new NotFoundException("Wrong email, phone number or password");
            }

            return _mapper.Map<GetUserDto>(user);
        }
    }
}
