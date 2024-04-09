using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IStudentClassTuitionRepository : IGenericRepository<StudentClassTuition>
    {
        Task<List<StudentClassTuition>> GetStudentClassTuitionsByStudentIdAndClassIdAsync(long studentId, long classId);
    }
}
