using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Helper.Exceptions;
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
            mappedEntranceTest.IsAnnouced = false;

            var createdEntranceTest = (await _entranceTestRepository.AddAsync(mappedEntranceTest))
                                    .Adapt<GetEntranceTestDto>();

            var criterias = await _criteriaService.GetEntranceTestCriteria();
            foreach (var c in criterias)
            {
                await _entranceTestResultService.CreateEntranceTestResult(new CreateEntranceTestResultDto
                {
                    CriteriaId = c.Id,
                    EntranceTestId = createdEntranceTest.Id,
                    Year = DateTime.Now.Year
                });
            }
            return createdEntranceTest;
        }

        public async Task<GetEntranceTestDto?> GetEntranceTestByStudentId(long studentId, bool isRequired)
        {
            var result = (await _entranceTestRepository.FindOneAsync(et => et.StudentId == studentId))?.Adapt<GetEntranceTestDto>();
            if (result is null && isRequired)
            {
                throw new NotFoundException("Entrance Test not found");
            }
            return result;
        }
    }
}
