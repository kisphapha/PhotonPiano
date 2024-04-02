using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IStudentClassRepository : IGenericRepository<StudentClass>
    {
        //Task<StudentClass?> GetStudentClassWithStudentLessonsById(long classId, long studentId);
    }
}
