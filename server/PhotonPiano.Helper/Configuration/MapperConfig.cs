using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhotonPiano.Helper.Dtos.User;
using PhotonPiano.Models;

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
