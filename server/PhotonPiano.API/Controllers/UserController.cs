using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotonPiano.Helper.Dtos.User;
using PhotonPiano.Models.Models;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<GetUserDto>> GetUsers()
        {
            var context = new PhotonPianoContext();
            var students = await context.Users.ToListAsync();
            return _mapper.Map<List<GetUserDto>>(students);
        }
    }
}
