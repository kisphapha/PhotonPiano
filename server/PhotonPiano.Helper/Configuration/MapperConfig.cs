using AutoMapper;
using PhotonPiano.Helper.Dtos.User;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            ConfigureUserMapping();
        }   

        private void ConfigureUserMapping()
        {
            CreateMap<User, GetUserDto>().ReverseMap();
        }
    }
}
