using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotonPiano.Models;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("user")]
        public async Task<List<User>> GetUsers() {
            var context = new PhotonPianoContext();
            return await context.Users.ToListAsync(); 
        }
        [HttpGet("classes")]
        public async Task<List<Class>> GetClasses()
        {
            var context = new PhotonPianoContext();
            return await context.Classes.ToListAsync();
        }
        [HttpGet("students")]
        public async Task<List<Student>> GetStudents()
        {
            var context = new PhotonPianoContext();
            return await context.Students.ToListAsync();
        }
        [HttpGet("instructors")]
        public async Task<List<Instructor>> GetInstructors()
        {
            var context = new PhotonPianoContext();
            return await context.Instructors.ToListAsync();
        }
        [HttpGet("lessons")]
        public async Task<List<Lesson>> GetLessons()
        {
            var context = new PhotonPianoContext();
            return await context.Lessons.ToListAsync();
        }
        [HttpGet("locations")]
        public async Task<List<Location>> GetLocations()
        {
            var context = new PhotonPianoContext();
            return await context.Locations.ToListAsync();
        }
        [HttpGet("student-classes")]
        public async Task<List<StudentClass>> GetStudentClasses()
        {
            var context = new PhotonPianoContext();
            return await context.StudentClasses.ToListAsync();
        }
        [HttpGet("student-class-scores")]
        public async Task<List<StudentClassScore>> GetStudentClassScores()
        {
            var context = new PhotonPianoContext();
            return await context.StudentClassScores.ToListAsync();
        }
        [HttpGet("student-lessons")]
        public async Task<List<StudentLesson>> GetStudentLessons()
        {
            var context = new PhotonPianoContext();
            return await context.StudentLessons.ToListAsync();
        }
        [HttpGet("student-class-tuitions")]
        public async Task<List<StudentClassTuition>> GetStudentClassTuitions()
        {
            var context = new PhotonPianoContext();
            return await context.StudentClassTuitions.ToListAsync();
        }
        [HttpGet("entrance-test")]
        public async Task<List<EntranceTest>> GetEntranceTests()
        {
            var context = new PhotonPianoContext();
            return await context.EntranceTests.ToListAsync();
        }
        [HttpGet("criteria")]
        public async Task<List<Criterion>> GetCriteria()
        {
            var context = new PhotonPianoContext();
            return await context.Criteria.ToListAsync();
        }
        [HttpGet("posts")]
        public async Task<List<Post>> GetPosts()
        {
            var context = new PhotonPianoContext();
            return await context.Posts.ToListAsync();
        }
        [HttpGet("comments")]
        public async Task<List<Comment>> GetComments()
        {
            var context = new PhotonPianoContext();
            return await context.Comments.ToListAsync();
        }
    }
}
