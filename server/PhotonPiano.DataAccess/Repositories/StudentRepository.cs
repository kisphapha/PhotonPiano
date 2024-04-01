
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly PhotonPianoContext _context;

        public StudentRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Student?> GetStudentDetailAsync(long id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                    .ThenInclude(s => s.Posts)
                .Include(s => s.StudentClasses)
                    .ThenInclude(sc => sc.Class)
                .Include(s => s.StudentClasses)
                    .ThenInclude(sc => sc.StudentClassTuitions)
                .Include(s => s.CurrentClass)
                    //.ThenInclude(c => c.Instructor)
                    //    .ThenInclude(i => i.User)
                .Include(s => s.StudentLessons)
                .Include(s => s.EntranceTests)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student?.CurrentClass is not null)
            {
                await _context.Entry(student.CurrentClass).Reference(c => c.Instructor)
                    .LoadAsync();               
                await _context.Entry(student.CurrentClass.Instructor).Reference(c => c.User)
                    .LoadAsync();           
            }


            //if (student?.CurrentClass is not null)
            //{
            //    var instructor = await _context.Classes
            //        .Include(c => c.Instructor)
            //            .ThenInclude(i => i.User)
            //        .Select(c => c.Instructor)
            //        .FirstOrDefaultAsync(c => c.Id == student.CurrentClass.Id);

            //    student.CurrentClass.Instructor = instructor!;
            //}
            return student;
        }
    }
}
