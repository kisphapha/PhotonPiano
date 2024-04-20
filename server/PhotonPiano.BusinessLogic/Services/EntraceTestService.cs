using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class EntranceTestSerivce : IEntranceTestService
    {
        private readonly IEntranceTestRepository _entranceTestRepository;
        private readonly IEntranceTestResultService _entranceTestResultService;
        private readonly IStudentService _studentService;
        private readonly ICriteriaSerivce _criteriaService;
        public EntranceTestSerivce(IEntranceTestRepository entranceTestRepository, 
            IStudentService studentService, ICriteriaSerivce criteriaSerivce,
            IEntranceTestResultService entranceTestResultService)
        {
            _entranceTestRepository = entranceTestRepository;
            _studentService = studentService;
            _criteriaService = criteriaSerivce;
            _entranceTestResultService = entranceTestResultService;
        }
        public async Task<GetEntranceTestDto> CreateEntranceTest(CreateEntranceTestDto createEntranceTestDto)
        {
            var checkStudent = await _studentService.CheckStudentExists(createEntranceTestDto.StudentId);
            if (!checkStudent)
            {
                throw new NotFoundException("Student not found");
            }
            if (await GetEntranceTestByStudentId(createEntranceTestDto.StudentId, false) is not null)
            {
                throw new NotFoundException("This student already has an entrance test!");
            }

            var mappedEntranceTest = createEntranceTestDto.Adapt<EntranceTest>();
            mappedEntranceTest.IsScoreAnnounced = false;
            mappedEntranceTest.Year = DateTime.Now.Year;

            var createdEntranceTest = (await _entranceTestRepository.AddAsync(mappedEntranceTest))
                                    .Adapt<GetEntranceTestDto>();

            var criterias = await _criteriaService.GetEntranceTestCriteria();
            foreach (var c in criterias)
            {
                await _entranceTestResultService.CreateEntranceTestResult(new CreateEntranceTestResultDto
                {
                    CriteriaId = c.Id,
                    EntranceTestId = createdEntranceTest.Id,                
                });
            }
            return createdEntranceTest;
        }
        public async Task<GetEntranceTestDto?> GetEntranceTestByStudentId(long studentId, bool isRequired)
        {
            var result = await _entranceTestRepository.FindOneAsync(et => et.StudentId == studentId);
            if (result is null && isRequired)
            {
                throw new NotFoundException("Entrance Test not found");
            }
            return result is not null ? result.Adapt<GetEntranceTestDto>() : null;
        }

        public async Task<EntranceTest> GetEntranceTestByRequiredId(long id)
        {
            var entranceTest = await _entranceTestRepository.GetByIdAsync(id);
            if (entranceTest is null)
            {
                throw new NotFoundException($"Entrance Test {id} not found");
            }
            return entranceTest;
        }

        public async Task UpdateEntranceTestId(long id, long slotId)
        {
            var entranceTest = await GetEntranceTestByRequiredId(id);
            entranceTest.EntranceTestSlotId = slotId;

            await _entranceTestRepository.UpdateAsync(entranceTest);
        }

        public async Task<GetEntranceTestWithResultDto> GetEntranceTestScoreOfAStudent(long studentId)
        {
            await _studentService.GetRequiredStudentById(studentId);
            return (await _entranceTestRepository.GetEntranceTestWithResultByStudentId(studentId)).Adapt<GetEntranceTestWithResultDto>();
        }
    
        public async Task<List<GetEntranceTestDto>> GetEntranceTestsByYear(int year)
        {
            return (await _entranceTestRepository.FindAsync(et => et.Year == year)).Adapt<List<GetEntranceTestDto>>();
        }

        public async Task AutoAcceptRegistrations(int number)
        {
            if (number < 1)
            {
                throw new BadRequestException("The number must be larger than 0");
            }
            var registrations = (await _studentService.GetPagedStudentList(1, 1, new QueryStudentDto
            {
                Status = StudentStatus.PendingRegistration.ToString(),
            }));
            var studentIds = new List<long>();
            for (int i = 0; i < Math.Min(number, registrations.TotalRecords); i++)
            {
                foreach(var studentDto in registrations.Results)
                {
                    studentIds.Add(studentDto.Id);
                    await CreateEntranceTest(new CreateEntranceTestDto()
                    {
                        StudentId = studentDto.Id
                    });
                }
            }
            await _studentService.ChangeStatusOfStudentInBatch(new UpdateStudentStatusInBatchDto
            {
                StudentIds = studentIds,
                Status = StudentStatus.Accepted.ToString(),
            });
        }
    }
}
