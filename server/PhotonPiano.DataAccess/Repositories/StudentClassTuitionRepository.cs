
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.StudentClassTuitons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class StudentClassTuitionRepository : GenericRepository<StudentClassTuition>, IStudentClassTuitionRepository
    {
        private readonly PhotonPianoContext _context;
        public StudentClassTuitionRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<StudentClassTuition>> GetStudentClassTuitionsByStudentIdAndClassIdAsync(long studentId, long classId)
        {
            var studentClassTuitions = await _context.StudentClassTuitions
                .Include(sct => sct.StudentClass.Student)
                .Include(sct => sct.StudentClass.Class)
                .Where(sct => sct.StudentClass.StudentId == studentId && sct.StudentClass.ClassId == classId)
                .ToListAsync();

            return studentClassTuitions;
        }
    }
}
