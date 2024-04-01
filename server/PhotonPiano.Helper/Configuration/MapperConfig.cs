using AutoMapper;
using PhotonPiano.Helper.Dtos.StudentClasses;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            ConfigureUserMapping();
            ConfigureStudentMapping();
        }   

        private void ConfigureUserMapping()
        {
            CreateMap<User, GetUserDto>().ReverseMap();
        }

        private void ConfigureStudentMapping()
        {
            CreateMap<Student, GetStudentDto>().ReverseMap();
        }
    }
}
