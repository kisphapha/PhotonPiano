using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.DataAccess.Repositories;
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Criteria;
using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class ClassSerivce : IClassService
    {
        private readonly IClassRepostiory _classRepostiory;
        private readonly IStudentService _studentService;
        private readonly IInstructorService _instructorService;
        public ClassSerivce(IClassRepostiory classRepostiory, IStudentService studentService, IInstructorService instructorService)
        {
            _classRepostiory = classRepostiory;
            _studentService = studentService;
            _instructorService = instructorService;
        }
        public async Task<Class> GetRequiredClassById(long id)
        {
            var class_ = await _classRepostiory.FindOneAsync(c => c.Id == id);
            if (class_ is null)
            {
                throw new NotFoundException($"Not found class id {id}");
            }
            return class_;
        }
        public async Task<GetClassDetailDto> GetClassDetail(long classId)
        {
            var class_ = await _classRepostiory.GetClassDetailAsync(classId);
            if (class_ is null)
            {
                throw new NotFoundException("Class not found");
            }
            return class_.Adapt<GetClassDetailDto>();
        }

        public async Task<PaginatedResult<GetClassWithTotalsDto>> GetPagedClasses(int pageNumber, int pageSize, QueryClassDto queryClassDto)
        {
            var classes = (await _classRepostiory.GetPagedClass(pageNumber, pageSize, queryClassDto));
            var mappedClasses = classes.Adapt<PaginatedResult<GetClassWithTotalsDto>>();
            foreach (var mappedClass in mappedClasses.Results)
            {
                mappedClass.TotalLessons = classes.Results.FirstOrDefault(c => c.Id == mappedClass.Id)?.Lessons.Count ?? 0;
                mappedClass.TotalStudents = classes.Results.FirstOrDefault(c => c.Id == mappedClass.Id)?.Students.Count ?? 0;
            }
            return mappedClasses;
        }

        public async Task AnnounceAClass(long id)
        {
            var class_ = await GetRequiredClassById(id);
            class_.IsAnnouced = true;
            await _classRepostiory.UpdateAsync(class_);
        }

        public async Task AnnounceAllClass()
        {
            var classes = await _classRepostiory.FindAsync(c => c.IsAnnouced == false);
            foreach (var class_ in classes)
            {
                class_.IsAnnouced = true;
            }
            await _classRepostiory.UpdateRangeAsync(classes);
        }

        public async Task<List<Class>> GetClassesBasedOnOption(ScheduleClassesOption option)
        {
            if (option == ScheduleClassesOption.All)
            {
                return await _classRepostiory.GetClassesWithLessons(null, null);
            }
            if (option == ScheduleClassesOption.Unscheduled)
            {
                return await _classRepostiory.GetClassesWithLessons(0, 0);
            }
            return new List<Class>();
        }

        public async Task<GetClassDto> CreateClass(CreateClassDto createClassDto)
        {
            await _instructorService.GetRequiredInstructorById(createClassDto.InstructorId);
            var mappedClass = createClassDto.Adapt<Class>();
            mappedClass.Status = ClassStatus.Active.ToString();
            return (await _classRepostiory.AddAsync(mappedClass)).Adapt<GetClassDto>();
        }
        public async Task UpdateClass(UpdateClassDto updateClassDto)
        {
            var class_ = await GetRequiredClassById(updateClassDto.Id);
            if (updateClassDto.Name != null)
            {
                class_.Name = updateClassDto.Name;
            }
            if (updateClassDto.Level.HasValue)
            {
                class_.Level = updateClassDto.Level.Value;
            }
            if (updateClassDto.Status != null)
            {
                class_.Status = updateClassDto.Status;
            }
            if (updateClassDto.StartDate.HasValue)
            {
                class_.StartDate = updateClassDto.StartDate.Value;
            }
            if (updateClassDto.InstructorId.HasValue)
            {
                var instructor = await _instructorService.GetRequiredInstructorById(updateClassDto.InstructorId.Value);
                class_.InstructorId = instructor.Id;
            }
            if (updateClassDto.Size.HasValue)
            {
                class_.Size = updateClassDto.Size.Value;
            }
        }

        public async Task UpdateClassEndDate(long classId)
        {
            var class_ = await GetClassDetail(classId);
            var latestLesson = class_.Lessons.OrderByDescending(l => l.Date).FirstOrDefault();
            class_.EndDate = latestLesson?.Date;
            await _classRepostiory.UpdateAsync(class_.Adapt<Class>());
        }
    }
}
