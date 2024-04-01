using AutoMapper;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<GetStudentProfileDto?> GetStudentDetailById(long id)
        {
            var student =  await _studentRepository.GetStudentDetailAsync(id);
            if (student is null)
            {
                throw new NotFoundException("Student not found");
            }
            //var studentInfo = _mapper.Map<GetStudentDto>(student);
            //var userInfo = _mapper.Map<GetUserDto>(student.User);
            //var studentProfile = new GetStudentProfileDto()
            //{
            //    UserInfo = userInfo,
            //    StudentInfo = studentInfo,
            //    InstructorName = student?.CurrentClass?.Instructor?.User?.Name,
            //};
            return student.Adapt<GetStudentProfileDto>();
        }

        public async Task<GetStudentWithPostsDto?> GetStudentWithPostsAndComments(long id)
        {
            var student = await _studentRepository.GetStudentWithPostsAndComments(id);
            if (student is null)
            {
                throw new NotFoundException("Student not found");
            }
            return student.Adapt<GetStudentWithPostsDto>();
        }
    }
}
