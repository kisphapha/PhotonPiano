using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.BusinessLogic.Services;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetUserDto>>> GetUsers()
        {
            return await _userService.GetUsers();
        }


        //[HttpPost]
        //public async Task<GetUserDto> Login([FromBody] LoginDto loginDto)
        //{
        //    return await _userService.VerifyLogin(loginDto.EmailOrPhone, loginDto.Password);
        //}
    }
}
