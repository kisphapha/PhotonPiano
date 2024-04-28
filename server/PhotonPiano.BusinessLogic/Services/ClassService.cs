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
        public async Task<GetClassDetailDto> GetClassDetail(long classId)
        {
            var class_ = await _classRepostiory.GetClassDetailAsync(classId);
            if (class_ is null)
            {
                throw new NotFoundException("Class not found");
            }
            return class_.Adapt<GetClassDetailDto>();
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
    }
}
