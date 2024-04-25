using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.DataAccess.Repositories;
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Criteria;
using PhotonPiano.Helper.Dtos.Paginations;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class ClassSerivce : IClassService
    {
        private readonly IClassRepostiory _classRepostiory;
        public ClassSerivce(IClassRepostiory classRepostiory)
        {
            _classRepostiory = classRepostiory;
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
        public async Task<GetClassWithInstructorAndLessonsDto> GetClassDetail(long classId)
        {
            var class_ = await _classRepostiory.GetClassDetailAsync(classId);
            if (class_ is null)
            {
                throw new NotFoundException("Class not found");
            }
            return class_.Adapt<GetClassWithInstructorAndLessonsDto>();
        }

        public async Task<PaginatedResult<GetClassWithTotalLessonDto>> GetPagedClasses(int pageNumber, int pageSize, QueryClassDto queryClassDto)
        {
            var classes = (await _classRepostiory.GetPagedClass(pageNumber, pageSize, queryClassDto));
            var mappedClasses = classes.Adapt<PaginatedResult<GetClassWithTotalLessonDto>>();
            foreach (var mappedClass in mappedClasses.Results)
            {
                mappedClass.TotalLessons = classes.Results.FirstOrDefault(c => c.Id == mappedClass.Id)?.Lessons.Count ?? 0;
            }
            return mappedClasses;
        }
    }
}
