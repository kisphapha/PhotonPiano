using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Auth;
using PhotonPiano.Helper.Dtos.User;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhotonPiano.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
        }
        public string CreateToken(long userId, string userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("uid", userId.ToString()),
                    new Claim(ClaimTypes.Role, userRole)
                }),
                Expires = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtSettings:DurationInDays"])),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userService.VerifyLogin(loginDto.EmailOrPhone, loginDto.Password);
            return new AuthResponseDto
            {
                Token = CreateToken(user.Id, user.Role)
            };
        }

        public async Task<GetUserDto> GetUserByClaims(ClaimsPrincipal claims)
        {
            var userId = claims.FindFirst(c => c.Type == "uid")?.Value;

            if (userId == null)
            {
                throw new UnauthorizedException("Not found user");
            }

            var user = await _userService.GetUserById(Convert.ToInt64(userId));

            if (user == null)
            {
                throw new UnauthorizedException("Not found user");
            }

            return user;
        }

    }
}
