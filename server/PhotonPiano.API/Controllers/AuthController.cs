using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.BusinessLogic.Services;
using PhotonPiano.Helper.Dtos.Auth;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            return await _authService.Login(loginDto);
        }
        [HttpGet("who-am-i")]
        [Authorize]
        public async Task<ActionResult<GetLoginedUserDto>> WhoAmI()
        {
            return await _authService.GetUserByClaims(HttpContext.User);
        }

        [HttpPost("register")]
        public async Task<ActionResult<GetUserWithStudentDto>> Register([FromBody] CreateUserDto body)
        {
            return await _userService.CreateUser(body);
        }
    }
}
