

using PhotonPiano.Helper.Dtos.StudentClassTuitons;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IStudentClassTuitionService
    {
        Task<List<GetStudentClassTuitionDto>> GetStudentClassTuitionsByStudentIdAndClassId(long studentId, long classId);
    }
}
