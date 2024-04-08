using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IEntranceTestRepository : IGenericRepository<EntranceTest>
    {
        Task<EntranceTest?> GetEntranceTestWithResultByStudentId(long studentId);
    }
}
