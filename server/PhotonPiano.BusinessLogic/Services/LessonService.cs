using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.DataAccess.Repositories;
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;
using System.Net.WebSockets;


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

        public async Task<List<GetLessonDetailDto>> GetQueriedLessons(QueryLessonDto queryLessonDto)
        {
            return (await _lessonRepository.GetQueriedLessonsAsync(queryLessonDto)).Adapt<List<GetLessonDetailDto>>();
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
    
        public async Task<AutoArrangeResultDto> AutoScheduleAClass(AutoArrangeLessonAClassDto autoArrangeLessonAClassDto)
        {
            //=================== filter =================== 
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
            //=================== Arrange =================== 
            //This list for all week of the year containing first and last day of that week. This is 
            //used to set the date for different weeks of the lessons
            var weeks = _ultilities.GetAllWeeksInYear(autoArrangeLessonAClassDto.StartingFrom.Year);
            //This is the pointer of the current week
            var startWeek = weeks.FirstOrDefault(w => w.StartDate <= autoArrangeLessonAClassDto.StartingFrom
                && w.EndDate >= autoArrangeLessonAClassDto.StartingFrom) ?? weeks[0];
            //Determine where the current week are in the weeks array
            var weekIndex = weeks.IndexOf(startWeek);

            long randomLocationId = 0;
            int randomShift = 0;
            DateOnly randomDate = new DateOnly();
            //The frame that contain lessons of the first week and scaffold the remaining weeks
            //if shift consistency option is turn on
            var lessonFrames = new List<GetLessonDto>();
            var rand = new Random();
            //Sunday is the first day, saturday is the last day 😏
            int includeSaturday = autoArrangeLessonAClassDto.OptionIncludeSaturday ? 0 : -1;
            int includeSunday = autoArrangeLessonAClassDto.OptionIncludeSunday ? 0 : 1;
            int lessonCount = 0;
            //=================== Action =================== 

            //Loop per week
            for (var i = 0; i < autoArrangeLessonAClassDto.TotalWeeks; i++)
            {
                //If this full week is day offs, go to next week
                if (IsThisWeekOff(startWeek.StartDate,autoArrangeLessonAClassDto.DayOffs,
                    autoArrangeLessonAClassDto.OptionIncludeSaturday, autoArrangeLessonAClassDto.OptionIncludeSunday))
                {
                    goto GotoNextWeek;
                }
                long locationThisWeek = 0;
                //Low per lesson per week
                for (var j = 0; j < autoArrangeLessonAClassDto.LessonEachWeek; j++)
                {
                    var isNew = true;
                    var attempt = 0;
                    //Arrange first week
                    if ((i == 0 && autoArrangeLessonAClassDto.OptionShiftConsistency) ||
                        (i == 0 && autoArrangeLessonAClassDto.OptionLocationConsistency) ||
                        !(autoArrangeLessonAClassDto.OptionShiftConsistency &&
                        autoArrangeLessonAClassDto.OptionLocationConsistency))
                    {
                        //Randomly assign shift, date and location for a lesson
                        do
                        {
                            //The location can be random or assign to locationThisWeek
                            //if it is determined
                            randomLocationId = (locationThisWeek == 0 ) ? 
                                autoArrangeLessonAClassDto.AllowedLocationIds[rand.Next(autoArrangeLessonAClassDto.AllowedLocationIds.Count)] :
                                locationThisWeek;
                            randomShift = autoArrangeLessonAClassDto.AllowedShift[rand.Next(autoArrangeLessonAClassDto.AllowedShift.Count)];
                            //If not includeSunday, plus one day; if not include saturday, minus one day
                            randomDate = _ultilities.GetRandomDateBetween(
                                startWeek.StartDate.AddDays(includeSunday), startWeek.EndDate.AddDays(includeSaturday));
                            //Avoid infinite loop since there can be no possible combination that not
                            //causing any conflicts
                            attempt++;
                            if (attempt > 100)
                            {
                                goto GotoNextWeek;
                                //throw new BadRequestException("Unable to continue arranging due to unavoidable conflicts. " +
                                //    "Please extend the range to increase the chance of success!");
                            }
                        }
                        //This will ensure the the combination won't cause any conflict. If it does,
                        //shuffle again
                        while (!await CheckLessonConflict(randomShift, randomLocationId, autoArrangeLessonAClassDto.ClassId, randomDate)
                         || autoArrangeLessonAClassDto.DayOffs.Contains(randomDate));
                        
                        //If option location similar is true, proceed to set locationThisWeek to the first
                        //location selected so that remaining location in this week is the same.
                        if (autoArrangeLessonAClassDto.OptionLocationSimilar)
                        {
                            locationThisWeek = randomLocationId;
                        } 
                        
                    }
                    //Arrange remaining weeks
                    if (i > 0 &&
                        autoArrangeLessonAClassDto.OptionShiftConsistency || autoArrangeLessonAClassDto.OptionLocationConsistency)
                    {
                        if (lessonFrames.Count > j)
                        {
                            //If option lcoation consistency is true, locations is the same as first week
                            if (autoArrangeLessonAClassDto.OptionLocationConsistency)
                            {
                                randomLocationId = lessonFrames[j].LocationId;
                            }
                            else
                            {
                                randomLocationId = (locationThisWeek == 0) ?
                                    autoArrangeLessonAClassDto.AllowedLocationIds[rand.Next(autoArrangeLessonAClassDto.AllowedLocationIds.Count)] :
                                    locationThisWeek;
                            }
                            //Assign shift,date & location if option shift consistency is set to true
                            if (autoArrangeLessonAClassDto.OptionShiftConsistency)
                            {
                                //Set shift & date to the same as the lessons in the first week
                                randomShift = lessonFrames[j].Shift;
                                randomDate = lessonFrames[j].Date.AddDays(i * 7);
                                isNew = false;

                            }
                        }
                        bool isNotFramed = !(lessonFrames.Count > j);
                        //If conflict happens, the rule of shift & location consistency will not be applied
                        //and will be shuffle again (for this lesson only)
                        while (!await CheckLessonConflict(randomShift, randomLocationId, autoArrangeLessonAClassDto.ClassId, randomDate)
                        || autoArrangeLessonAClassDto.DayOffs.Contains(randomDate) || isNotFramed) 
                        {
                            isNotFramed = false;
                            randomLocationId = (locationThisWeek == 0) ?
                                autoArrangeLessonAClassDto.AllowedLocationIds[rand.Next(autoArrangeLessonAClassDto.AllowedLocationIds.Count)] :
                                locationThisWeek;
                            randomShift = autoArrangeLessonAClassDto.AllowedShift[rand.Next(autoArrangeLessonAClassDto.AllowedShift.Count)];
                            randomDate = _ultilities.GetRandomDateBetween(
                                startWeek.StartDate.AddDays(includeSunday), startWeek.EndDate.AddDays(includeSaturday));
                                                           
                            attempt++;
                            if (attempt > 100)
                            {
                                goto GotoNextWeek;
                                //throw new BadRequestException("Unable to continue arranging due to unavoidable conflicts. " +
                                //    "Please extend the range to increase the chance of success!");
                            }                                                 
                        }                                           
                        
                    }
                    //If option location similar is true, proceed to set locationThisWeek to the first
                    //location selected so that remaining location in this week is the same.
                    if (autoArrangeLessonAClassDto.OptionLocationSimilar)
                    {
                        locationThisWeek = randomLocationId;
                    }
                    //Create lesson after checking all conflicts
                    await CreateLesson(new CreateLessonDto
                    {
                        ClassId = autoArrangeLessonAClassDto.ClassId,
                        Date = randomDate,
                        LocationId = randomLocationId,
                        Shift = randomShift,
                    });
                    lessonCount++;
                    //Create the frame to use for assigning lessons of remaining weeks
                    if (isNew)
                    {
                        lessonFrames.Add(new GetLessonDto
                        {
                            Id = j,
                            LocationId = randomLocationId,
                            Shift = randomShift,
                            //Back to the first week
                            Date = randomDate.AddDays(- (i * 7))
                        });
                    }             
                }
                //After everything, increase week index to calculate date for the next iteration
                //based on the weekInYear list
                GotoNextWeek:
                if (i < autoArrangeLessonAClassDto.TotalWeeks - 1)
                {
                    weekIndex++;
                    //Check for index out of bound
                    if (weekIndex >= weeks.Count)
                    {
                        throw new BadRequestException("Cannot complete due to not having enough weeks");
                    }
                    startWeek = weeks[weekIndex];
                }
                
            }
            return new AutoArrangeResultDto
            {
                LessonsAdded = lessonCount,
                LessonsEstimated = autoArrangeLessonAClassDto.TotalWeeks * autoArrangeLessonAClassDto.LessonEachWeek
            };
        }

        private bool IsThisWeekOff(DateOnly startDate, List<DateOnly> dayOffs, bool includeSaturday,
            bool includeSunday)
        {
            var dayOffsThisWeek = dayOffs.Where(d => d >= startDate && d <= startDate.AddDays(6)).ToList();
            var daysExpected = 5 + (includeSaturday ? 1 : 0) + (includeSunday ? 1 : 0);
            return dayOffsThisWeek.Count == daysExpected;
        }

        public async Task<AutoArrangeResultDto> AutoScheduleAllClass(AutoArrangeLessonAllClassDto autoArrangeLessonAllClassDto)
        {
            var classes = await _classService.GetClassesBasedOnOption(autoArrangeLessonAllClassDto.ClassesOption);
            var resultDto = new AutoArrangeResultDto
            {
                LessonsAdded = 0,
                LessonsEstimated = 0
            };
            foreach (var class_ in classes)
            {
                var dto = autoArrangeLessonAllClassDto.Adapt<AutoArrangeLessonAClassDto>();
                dto.ClassId = class_.Id;
                var result = await AutoScheduleAClass(dto);
                resultDto.LessonsAdded += result.LessonsAdded;
                resultDto.LessonsEstimated += result.LessonsEstimated;
            }
            return resultDto;
        }
    }
}
