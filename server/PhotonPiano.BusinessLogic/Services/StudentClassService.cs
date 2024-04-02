
using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.StudentClasses;
using PhotonPiano.Helper.Exceptions;

namespace PhotonPiano.BusinessLogic.Services
{
    public class StudentClassService : IStudentClassService
    {
        private readonly IStudentClassRepository _studentClassRepository;

        public StudentClassService(IStudentClassRepository studentClassRepository)
        {
            _studentClassRepository = studentClassRepository;
        }

        //public async Task<GetStudentClassWithStudentLessonsDto?> GetStudentClassWithStudentLessons(long classId, long studentId)
        //{
        //    var result = (await _studentClassRepository.GetStudentClassWithStudentLessonsById(classId,studentId)).Adapt<GetStudentClassWithStudentLessonsDto>();
        //    if (result is null)
        //    {
        //        throw new NotFoundException("Student or Class not found");
        //    }
        //    return result;
        //}
    }
}
