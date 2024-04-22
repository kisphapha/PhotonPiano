
using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class EntranceTestSlotSerivce : IEntranceTestSlotService
    {
        private readonly IEntranceTestSlotRepository _entranceTestSlotRepository;
        private readonly IEntranceTestService _entranceTestService;
        private readonly IStudentService _studentService;
        private readonly ILocationService _locationService;

        public EntranceTestSlotSerivce(IEntranceTestSlotRepository entranceTestSlotRepository, 
            ILocationService locationSerivce, IEntranceTestService entranceTestService, IStudentService studentService)
        {
            _entranceTestSlotRepository = entranceTestSlotRepository;
            _locationService = locationSerivce;
            _entranceTestService = entranceTestService;
            _studentService = studentService;
        }

        public async Task<GetEntranceTestSlotDetailDto> GetEntranceTestSlotDetail(long id)
        {
            var entranceTestSlot = await _entranceTestSlotRepository.GetEntranceTestSlotDetail(id);
            if (entranceTestSlot is null)
            {
                throw new NotFoundException($"Entrance test slot {id} not found");
            }
            return entranceTestSlot.Adapt<GetEntranceTestSlotDetailDto>();
        }
        public async Task<EntranceTestSlot> GetRequiredEntranceTestSlotById(long id)
        {
            var entranceTestSlot = await _entranceTestSlotRepository.GetByIdAsync(id);
            if (entranceTestSlot is null)
            {
                throw new NotFoundException($"Entrance test slot {id} not found");
            }
            return entranceTestSlot;
        }

        public async Task<List<GetEntranceTestSlotWithLocationDto>> GetEntranceTestSlotsByYear(int year)
        {
            var result = await _entranceTestSlotRepository.GetEntranceTestSlotsByYear(year);
            return result.Adapt<List<GetEntranceTestSlotWithLocationDto>>();
        }

        public async Task<GetEntranceTestSlotDto> CreateEntranceTestSlot(CreateEntranceTestSlotDto createEntranceTestSlotDto)
        {
            var location = await _locationService.GetLocationById(createEntranceTestSlotDto.LocationId,true);
            if (location!.Status == LocationStatus.Unavailable.ToString())
            {
                throw new BadRequestException("This location is currently unavailable");
            }

            var mappedEntranceTestSlot = createEntranceTestSlotDto.Adapt<EntranceTestSlot>();
            mappedEntranceTestSlot.IsAnnoucedTime = false;

            var createdSlot = (await _entranceTestSlotRepository.AddAsync(mappedEntranceTestSlot)).Adapt<GetEntranceTestSlotDto>();     

            return createdSlot;
        }
        public async Task ClearEntranceTestInASlot(long slotId)
        {
            var entranceTestSlot = await GetEntranceTestSlotDetail(slotId);
            foreach (var entranceTest in entranceTestSlot.EntranceTests)
            {
                await _entranceTestService.UpdateEntranceTestId(entranceTest.Id, null);
                await _studentService.ChangeStatusOfStudent(entranceTest.Student.Id, StudentStatus.Accepted.ToString());
            }
        }
        public async Task UpsertEntranceTestToEntranceTestSlot(AddEntranceTestToASlotDto addEntranceTestToASlotDto)
        {
            await GetRequiredEntranceTestSlotById(addEntranceTestToASlotDto.SlotId);
            await ClearEntranceTestInASlot(addEntranceTestToASlotDto.SlotId);
            foreach (var entranceTestId in addEntranceTestToASlotDto.EntranceTestIds)
            {              
                await _entranceTestService.UpdateEntranceTestId(entranceTestId, addEntranceTestToASlotDto.SlotId);               
            }
        }
        public async Task UpsertStudentsToEntranceTestSlot(AddStudentsToASlotDto addStudentsToASlot)
        {
            await GetRequiredEntranceTestSlotById(addStudentsToASlot.SlotId);
            await ClearEntranceTestInASlot(addStudentsToASlot.SlotId);
            foreach (var studentId in addStudentsToASlot.StudentIds)
            {
                var entranceTest = await _entranceTestService.GetUnScoreEntranceTestByStudentId(studentId);
                if (entranceTest != null)
                {
                    await _entranceTestService.UpdateEntranceTestId(entranceTest.Id, addStudentsToASlot.SlotId);
                    await _studentService.ChangeStatusOfStudent(studentId, StudentStatus.EntranceTesting.ToString());
                }
            }

        }
        public async Task AnnouceEntranceTestSlot(long slotId)
        {
            var slot = await GetRequiredEntranceTestSlotById(slotId);
            slot.IsAnnoucedTime = true;
            slot.AnnounceTime = DateTime.Now;

            await _entranceTestSlotRepository.UpdateAsync(slot);
        }

    }
}
