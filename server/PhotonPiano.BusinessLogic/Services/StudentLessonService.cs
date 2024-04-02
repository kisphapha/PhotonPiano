using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.StudentLessons;

namespace PhotonPiano.BusinessLogic.Services
{
    public class StudentLessonService : IStudentLessonService
    {
        private readonly IStudentLessonRepository _studentLessonRepository;
        public StudentLessonService(IStudentLessonRepository studentLessonRepository)
        {
            _studentLessonRepository = studentLessonRepository;
        }

        public async Task<List<GetStudentLessonDto>> GetStudentLessonsByClassIdAndStudentId(long classId, long studentId)
        {
            var studentLessons = await _studentLessonRepository.GetStudentLessonsByClassIdAndStudentIdAsync(classId, studentId);
            var studentLessonsDto = studentLessons.Adapt<List<GetStudentLessonDto>>();
            return studentLessonsDto;
        }
    }
}
