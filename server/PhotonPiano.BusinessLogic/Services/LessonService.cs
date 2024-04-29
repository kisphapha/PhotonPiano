using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.DataAccess.Repositories;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;


namespace PhotonPiano.BusinessLogic.Services
{
    public class LessonSerivce : ILessonSerivce
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IClassService _classService;
        private readonly ILocationService _locationService;
        private readonly IStudentLessonService _studentLessonService;
        private readonly IUtilities _ultilities;
        public LessonSerivce(ILessonRepository lessonRepository, ILocationService locationService, 
            IClassService classService, IStudentLessonService studentLessonService, 
            IUtilities utilities)
        {
            _lessonRepository = lessonRepository;
            _locationService = locationService;
            _classService = classService;
            _studentLessonService = studentLessonService;
            _ultilities = utilities;
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
            if (!await CheckLessonConflict(createLessonDto.Shift, createLessonDto.LocationId, createLessonDto.ClassId, createLessonDto.Date))
            {
                throw new BadRequestException("Cannot create due to schedule conflict. Please check again!");
            }
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
            if (existedLesson.IsLocked)
            {
                throw new BadRequestException("This lesson has been finished and unable to change");
            }
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
            if (!await CheckLessonConflict(existedLesson.Shift, existedLesson.LocationId, existedLesson.ClassId, existedLesson.Date))
            {
                throw new BadRequestException("Cannot update due to schedule conflict. Please check again!");
            }
            await _lessonRepository.UpdateAsync(existedLesson);
        }

        public async Task DeleteLesson(long lessonId)
        {
            var lesson = await GetRequiredLessonById(lessonId);
            if (lesson.IsLocked)
            {
                throw new BadRequestException("This lesson has been finished and unable to change");
            }
            await _studentLessonService.ClearStudentLessonsByLessonId(lessonId);
            await _lessonRepository.DeleteAsync(lessonId);
        }

        public async Task ClearAllNotStartedLessonOfAClass(long classId)
        {
            var class_ = await _classService.GetClassDetail(classId);
            var unlockedLessons = class_.Lessons.Where(l => !l.IsLocked).ToList();
            foreach (var lesson in unlockedLessons)
            {
                await _studentLessonService.ClearStudentLessonsByLessonId(lesson.Id);
            }
            await _lessonRepository.DeleteRangeAsync(unlockedLessons.Adapt<List<Lesson>>());
        }
        public async Task ClearAllNotStartedLessonOfAllClass()
        {
            var unlockedLessons = await _lessonRepository.FindAsync(l => !l.IsLocked);
            foreach (var lesson in unlockedLessons)
            {
                await _studentLessonService.ClearStudentLessonsByLessonId(lesson.Id);
            }
            await _lessonRepository.DeleteRangeAsync(unlockedLessons);
        }
        public async Task<bool> CheckLessonConflict(int shift, long locationId, long classId, DateOnly date)
        {
            var classConflict = await _lessonRepository
                .FindOneAsync(l =>
                    l.ClassId == classId &&
                    l.Shift == shift &&
                    l.Date == date);

            var locationConflict = await _lessonRepository
                .FindOneAsync(l =>
                    l.LocationId == locationId &&
                    l.Shift == shift &&
                    l.Date == date);

            return classConflict is null && locationConflict is null;
        }
    
        public async Task AutoScheduleAClass(AutoArrangeLessonAClassDto autoArrangeLessonAClassDto)
        {
            //filter
            var class_ = await _classService.GetRequiredClassById(autoArrangeLessonAClassDto.ClassId);
            if (!autoArrangeLessonAClassDto.AllowedShift.All(s => s >= 1 && s <= 8))
            {
                throw new BadRequestException("Shifts can only be from range 1 to 8");
            }
            foreach (var locationId in autoArrangeLessonAClassDto.AllowedLocationIds)
            {
                var location = await _locationService.GetLocationById(locationId, true);
                if (location!.Status == LocationStatus.Unavailable.ToString())
                {
                    throw new BadRequestException($"Location {location.Id} is unavailable");
                }
            }

            var weeks = _ultilities.GetAllWeeksInYear(autoArrangeLessonAClassDto.StartingFrom.Year);
            var startWeek = weeks.FirstOrDefault(w => w.StartDate <= autoArrangeLessonAClassDto.StartingFrom
                && w.EndDate >= autoArrangeLessonAClassDto.StartingFrom) ?? weeks[0];
            var weekIndex = weeks.IndexOf(startWeek);
            //Arrange
            long randomLocationId = 0;
            int randomShift = 0;
            DateOnly randomDate = new DateOnly();
            var lessonFrames = new List<GetLessonDto>();
            var rand = new Random();
            //Action
            for (var i = 0; i < autoArrangeLessonAClassDto.TotalWeeks; i++)
            {
                long locationThisWeek = 0;
                for (var j = 0; j < autoArrangeLessonAClassDto.LessonEachWeek; j++)
                {
                    var attempt = 0;
                    //Arrange first week
                    if ((i == 0 && autoArrangeLessonAClassDto.OptionShiftConsistency) ||
                        (i == 0 && autoArrangeLessonAClassDto.OptionLocationConsistency) ||
                        !(autoArrangeLessonAClassDto.OptionShiftConsistency &&
                        autoArrangeLessonAClassDto.OptionLocationConsistency))
                    {
                        do
                        {
                            randomLocationId = (locationThisWeek == 0 ) ? 
                                autoArrangeLessonAClassDto.AllowedLocationIds[rand.Next(autoArrangeLessonAClassDto.AllowedLocationIds.Count)]
                                : locationThisWeek;
                            randomShift = autoArrangeLessonAClassDto.AllowedShift[rand.Next(autoArrangeLessonAClassDto.AllowedShift.Count)];
                            randomDate = _ultilities.GetRandomDateBetween(startWeek.StartDate, startWeek.EndDate);
                            attempt++;
                            if (attempt > 100)
                            {
                                throw new BadRequestException("Unable to continue arranging due to unavoidable conflicts. " +
                                    "Please extend the range to increase the chance of success!");
                            }
                        }
                        while (!await CheckLessonConflict(randomShift, randomLocationId, autoArrangeLessonAClassDto.ClassId, randomDate));
                        if (autoArrangeLessonAClassDto.OptionLocationSimilar)
                        {
                            locationThisWeek = randomLocationId;
                        }                        
                        lessonFrames.Add(new GetLessonDto
                        {
                            Id = j,
                            LocationId = randomLocationId,
                            Shift = randomShift,
                            Date = randomDate
                        }); ;
                    }
                    //Arrange remaining weeks
                    if (i > 0 &&
                        autoArrangeLessonAClassDto.OptionShiftConsistency || autoArrangeLessonAClassDto.OptionLocationConsistency)
                    {
                        if (autoArrangeLessonAClassDto.OptionShiftConsistency)
                        {
                            randomLocationId = (locationThisWeek == 0) ?
                                autoArrangeLessonAClassDto.AllowedLocationIds[rand.Next(autoArrangeLessonAClassDto.AllowedLocationIds.Count)]
                                : locationThisWeek;
                            randomShift = lessonFrames[j].Shift;
                            randomDate = lessonFrames[j].Date.AddDays(i * 7);
                        }
                        if (autoArrangeLessonAClassDto.OptionLocationConsistency)
                        {
                            randomLocationId = lessonFrames[j].LocationId;
                        }
                        
                        while (!await CheckLessonConflict(randomShift, randomLocationId, autoArrangeLessonAClassDto.ClassId, randomDate))
                        {
                            randomLocationId = autoArrangeLessonAClassDto.AllowedLocationIds[rand.Next(autoArrangeLessonAClassDto.AllowedLocationIds.Count)];
                            randomShift = autoArrangeLessonAClassDto.AllowedShift[rand.Next(autoArrangeLessonAClassDto.AllowedShift.Count)];
                            randomDate = _ultilities.GetRandomDateBetween(startWeek.StartDate, startWeek.EndDate);
                            attempt++;
                            if (attempt > 100)
                            {
                                throw new BadRequestException("Unable to continue arranging due to unavoidable conflicts. " +
                                    "Please extend the range to increase the chance of success!");
                            }
                        }
                        if (autoArrangeLessonAClassDto.OptionLocationSimilar)
                        {
                            locationThisWeek = randomLocationId;
                        }
                    }

                    await CreateLesson(new CreateLessonDto
                    {
                        ClassId = autoArrangeLessonAClassDto.ClassId,
                        Date = randomDate,
                        LocationId = randomLocationId,
                        Shift = randomShift,
                    });
                }
                if (i < autoArrangeLessonAClassDto.TotalWeeks - 1)
                {
                    weekIndex++;
                    if (weekIndex >= weeks.Count)
                    {
                        throw new BadRequestException("Cannot complete due to not having enough weeks");
                    }
                    startWeek = weeks[weekIndex];
                }
                
            }          
        }
    }
}
