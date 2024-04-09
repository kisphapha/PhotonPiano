using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Criteria;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;

namespace PhotonPiano.BusinessLogic.Services
{
    public class ClassSerivce : IClassService
    {
        private readonly IClassRepostiory _classRepostiory;
        public ClassSerivce(IClassRepostiory classRepostiory)
        {
            _classRepostiory = classRepostiory;
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
    }
}
