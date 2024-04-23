using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Instructors;
using PhotonPiano.Helper.Dtos.Paginations;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class InstructorSerivce : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        public InstructorSerivce(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<Instructor> GetRequiredInstructorById(long id)
        {
            var instructor = await _instructorRepository.FindOneAsync(i => i.Id == id);
            if (instructor is null)
            {
                throw new NotFoundException($"Instructor {id} not found");
            }
            return instructor;
        }
        public async Task<PaginatedResult<GetInstructorWithUserDto>> GetPagedInstructors(int pageNumber, int pageSize, QueryInstructorDto queryInstructorDto)
        {
            return (await _instructorRepository.GetPagedInstructors(pageNumber, pageSize, queryInstructorDto)).Adapt<PaginatedResult<GetInstructorWithUserDto>>();
        }
    }
}
