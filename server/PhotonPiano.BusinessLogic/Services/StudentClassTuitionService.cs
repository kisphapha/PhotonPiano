using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.StudentClassTuitons;
namespace PhotonPiano.BusinessLogic.Services
{
    public class StudentClassTuitionService : IStudentClassTuitionService
    {
        private readonly IStudentClassTuitionRepository _studentClassTuitionRepository;
        public StudentClassTuitionService(IStudentClassTuitionRepository studentClassTuitionRepository)
        {
            _studentClassTuitionRepository = studentClassTuitionRepository;
        }

        public async Task<List<GetStudentClassTuitionDto>> GetStudentClassTuitionsByStudentIdAndClassId(long studentId, long classId)
        {
            return (await _studentClassTuitionRepository.GetStudentClassTuitionsByStudentIdAndClassIdAsync(studentId, classId))
                .Adapt<List<GetStudentClassTuitionDto>>();
        }
    }
}
