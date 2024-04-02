﻿using AutoMapper;
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
        private readonly IStudentLessonService _studentLessonService;
        public StudentService(IStudentRepository studentRepository, IStudentLessonService studentLessonService)
        {
            _studentRepository = studentRepository;
            _studentLessonService = studentLessonService;
        }

        public async Task<GetStudentProfileDto?> GetStudentDetailById(long id)
        {
            var student =  await _studentRepository.GetStudentDetailAsync(id);
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