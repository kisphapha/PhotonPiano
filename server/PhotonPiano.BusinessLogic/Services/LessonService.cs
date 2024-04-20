using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Helper.Exceptions;


namespace PhotonPiano.BusinessLogic.Services
{
    public class LessonSerivce : ILessonSerivce
    {
        private readonly ILessonRepository _lessonRepository;
        public LessonSerivce(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<List<GetLessonWithLocationDto>> GetQueriedLessons(QueryLessonDto queryLessonDto)
        {
            return (await _lessonRepository.GetQueriedLessonsAsync(queryLessonDto)).Adapt<List<GetLessonWithLocationDto>>();
        }
    }
}
