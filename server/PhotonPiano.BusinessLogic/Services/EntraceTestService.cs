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
            var createdEntranceTest = (await _entranceTestRepository.AddAsync(createEntranceTestDto.Adapt<EntranceTest>()))
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
    }
}
