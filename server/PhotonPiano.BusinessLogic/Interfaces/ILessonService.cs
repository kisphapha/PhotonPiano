using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface ILessonSerivce
    {
        Task<List<GetLessonDetailDto>> GetQueriedLessons(QueryLessonDto queryLessonDto);
        Task<Lesson> GetRequiredLessonById(long id);
        Task<GetLessonDto> CreateLesson(CreateLessonDto createLessonDto);
        Task UpdateLesson(UpdateLessonDto updateLessonDto);

        Task DeleteLesson(long lessonId);

        Task ClearAllNotStartedLessonOfAClass(long classId);

        Task ClearAllNotStartedLessonOfAllClass();

        Task<AutoArrangeResultDto> AutoScheduleAClass(AutoArrangeLessonAClassDto autoArrangeLessonAClassDto);

        Task<AutoArrangeResultDto> AutoScheduleAllClass(AutoArrangeLessonAllClassDto autoArrangeLessonAllClassDto);
    }
}
