using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Models;


namespace PhotonPiano.BusinessLogic.Services
{
    public class LessonSerivce : ILessonSerivce
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IClassService _classService;
        private readonly ILocationService _locationService;
        private readonly IStudentLessonService _studentLessonService;
        public LessonSerivce(ILessonRepository lessonRepository, ILocationService locationService, 
            IClassService classService, IStudentLessonService studentLessonService)
        {
            _lessonRepository = lessonRepository;
            _locationService = locationService;
            _classService = classService;
            _studentLessonService = studentLessonService;
        }

        public async Task<List<GetLessonWithLocationDto>> GetQueriedLessons(QueryLessonDto queryLessonDto)
        {
            return (await _lessonRepository.GetQueriedLessonsAsync(queryLessonDto)).Adapt<List<GetLessonWithLocationDto>>();
        }
        public async Task<Lesson> GetRequiredLessonById(long id)
        {
            var lesson = await _lessonRepository.FindOneAsync(l => l.Id == id);
            if (lesson is null)
            {
                throw new NotFoundException($"Not found lesson id {id}");
            }
            return lesson;
        }
        public async Task<GetLessonDto> CreateLesson(CreateLessonDto createLessonDto)
        {
            await _locationService.GetLocationById(createLessonDto.LocationId, true);
            var class_ = await _classService.GetClassDetail(createLessonDto.ClassId);

            var createLesson = await _lessonRepository.AddAsync(createLessonDto.Adapt<Lesson>());

            foreach (var student in class_.Students)
            {
                await _studentLessonService.AddStudentLesson(student.Id, createLesson.Id);
            }

            return createLesson.Adapt<GetLessonDto>();
        }
        
        public async Task UpdateLesson(UpdateLessonDto updateLessonDto)
        {
            var existedLesson = await GetRequiredLessonById(updateLessonDto.Id);
            if (updateLessonDto.ClassId.HasValue)
            {
                await _classService.GetRequiredClassById(updateLessonDto.ClassId.Value);
                existedLesson.ClassId = updateLessonDto.ClassId.Value;
            }
            if (updateLessonDto.LocationId.HasValue)
            {
                await _locationService.GetLocationById(updateLessonDto.LocationId.Value, true);
                existedLesson.LocationId = updateLessonDto.LocationId.Value;
            }
            if (updateLessonDto.Shift.HasValue)
            {
                existedLesson.Shift = updateLessonDto.Shift.Value;
            }
            if (updateLessonDto.Date != null)
            {
                existedLesson.Date = updateLessonDto.Date.Value;
            }
            if (updateLessonDto.ExamType != null)
            {
                existedLesson.ExamType = updateLessonDto.ExamType;
            }
            await _lessonRepository.UpdateAsync(existedLesson);
        }

        public async Task DeleteLesson(long lessonId)
        {
            await GetRequiredLessonById(lessonId);
            await _studentLessonService.ClearStudentLessonsByLessonId(lessonId);
            await _lessonRepository.DeleteAsync(lessonId);
        }
    }
}
