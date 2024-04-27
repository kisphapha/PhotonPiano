using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.StudentLessons;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

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

        public async Task<List<GetStudentLessonWithLocationDto>> GetDetailStudentLessonsByClassIdAndStudentId(long studentId, long classId, QueryLessonDto queryLessonDto)
        {
            var studentLessons = await _studentLessonRepository.GetQueriedStudentLessonsByClassIdAndStudentIdAsync(studentId, classId, queryLessonDto);
            var studentLessonsDto = studentLessons.Adapt<List<GetStudentLessonWithLocationDto>>();
            return studentLessonsDto;
        }

        public async Task AddStudentLesson(long studentId, long lessonId)
        {
            var studentLesson = new StudentLesson
            {
                StudentId = studentId,
                LessonId = lessonId,
                Attendence = AttendingStatus.NotYet.ToString(),
            };
            await _studentLessonRepository.AddAsync(studentLesson);
        }

        public async Task<List<StudentLesson>> GetStudentLessonsByLessonId(long lessonId)
        {
            return (await _studentLessonRepository.FindAsync(sl => sl.LessonId == lessonId)).ToList();
        }

        public async Task ClearStudentLessonsByLessonId(long lessonId)
        {
            var studentLessons = await GetStudentLessonsByLessonId(lessonId);
            await _studentLessonRepository.DeleteRangeAsync(studentLessons);
        }
    }
}
