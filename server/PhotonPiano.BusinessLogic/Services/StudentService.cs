using AutoMapper;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Paginations;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentLessonService _studentLessonService;
        public StudentService(IStudentRepository studentRepository, IStudentLessonService studentLessonService)
        {
            _studentRepository = studentRepository;
            _studentLessonService = studentLessonService;
        }

        public async Task<bool> CheckStudentExists(long studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            return (student is not null) ;
        }
        public async Task<GetStudentProfileDto?> GetStudentDetailById(long id)
        {
            var student = await _studentRepository.GetStudentDetailAsync(id);
            if (student is null)
            {
                throw new NotFoundException("Student not found");
            }
            var mappedStudent = student.Adapt<GetStudentProfileDto>();
            foreach (var item in mappedStudent.StudentClasses)
            {
                item.StudentLessons = await _studentLessonService.GetStudentLessonsByClassIdAndStudentId(item.ClassId, item.StudentId);
            }
            return mappedStudent;
        }
        public async Task<Student> GetRequiredStudentById(long studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student is null)
            {
                throw new NotFoundException("Student not found");
            }
            return student;
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

        public async Task<GetStudentDto> CreateStudentAfterCreatedUser(long userId)
        {
            var student = new Student
            {
                Level = 0,
                Status = StudentStatus.Unregistered.ToString(),
                UserId = userId,
                RegistrationDate = DateTime.Now
            };
            return (await _studentRepository.AddAsync(student)).Adapt<GetStudentDto>();
        }

        public async Task ChangeStatusOfStudent(long studentId, string status)
        {
            var student = await GetRequiredStudentById(studentId);
            if (!Enum.TryParse<StudentStatus>(status, out var result))
            {
                throw new BadRequestException("Status invalid");
            }
            student.Status = status;
            await _studentRepository.UpdateAsync(student);
        }

        public async Task ChangeStatusOfStudentInBatch(UpdateStudentStatusInBatchDto updateStudentStatusInBatch)
        {
            var students = new List<Student>();

            //if (updateStudentStatusInBatch.Status == StudentStatus.Accepted.ToString())
            //{
            //    throw new BadRequestException("Please use automatically accept registrations end point instead");
            //}

            foreach (var id in updateStudentStatusInBatch.StudentIds)
            {
                var student = await GetRequiredStudentById(id);
                student.Status = updateStudentStatusInBatch.Status;
                students.Add(student);
            }         
            await _studentRepository.UpdateRangeAsync(students);
        }

        public async Task UpdateStudentShortDescription(long studentId, string desc)
        {
            var student = await GetRequiredStudentById(studentId);
            student.ShortDesc = desc;
            await _studentRepository.UpdateAsync(student);
        }
        public async Task<PaginatedResult<GetStudentWithUserDto>> GetPagedStudentList(int pageNumber, int pageSize, QueryStudentDto queryStudentDto)
        {
            return (await _studentRepository.GetPagedStudents(pageNumber,pageSize, queryStudentDto)).Adapt<PaginatedResult<GetStudentWithUserDto>>();
        }
    }
}
