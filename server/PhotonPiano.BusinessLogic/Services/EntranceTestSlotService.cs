
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
        private readonly IInstructorService _instructorService;

        public EntranceTestSlotSerivce(IEntranceTestSlotRepository entranceTestSlotRepository, 
            ILocationService locationSerivce, IEntranceTestService entranceTestService, 
            IStudentService studentService, IInstructorService instructorService)
        {
            _entranceTestSlotRepository = entranceTestSlotRepository;
            _locationService = locationSerivce;
            _entranceTestService = entranceTestService;
            _studentService = studentService;
            _instructorService = instructorService;
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
            if (createEntranceTestSlotDto.InstructorId.HasValue)
            {
                await _instructorService.GetRequiredInstructorById(createEntranceTestSlotDto.InstructorId.Value);
            }

            var mappedEntranceTestSlot = createEntranceTestSlotDto.Adapt<EntranceTestSlot>();
            mappedEntranceTestSlot.IsAnnoucedTime = false;

            var createdSlot = (await _entranceTestSlotRepository.AddAsync(mappedEntranceTestSlot)).Adapt<GetEntranceTestSlotDto>();     

            return createdSlot;
        }
        public async Task UpdateEntranceTestSlot(UpdateEntranceTestSlotDto updateEntranceTestSlotDto)
        {
            var existedSlot = await GetRequiredEntranceTestSlotById(updateEntranceTestSlotDto.Id);
            if (updateEntranceTestSlotDto.LocationId.HasValue)
            {
                var location = await _locationService.GetLocationById(updateEntranceTestSlotDto.LocationId.Value, true);
                if (location!.Status == LocationStatus.Unavailable.ToString())
                {
                    throw new BadRequestException("This location is currently unavailable");
                }
                existedSlot.LocationId = updateEntranceTestSlotDto.LocationId.Value;
            }
            if (updateEntranceTestSlotDto.InstructorId.HasValue)
            {
                await _instructorService.GetRequiredInstructorById(updateEntranceTestSlotDto.InstructorId.Value);
                existedSlot.InstructorId = updateEntranceTestSlotDto.InstructorId.Value;
            }
            if (updateEntranceTestSlotDto.Shift.HasValue)
            {
                existedSlot.Shift = updateEntranceTestSlotDto.Shift.Value;
            }
            if (updateEntranceTestSlotDto.Date.HasValue)
            {
                existedSlot.Date = updateEntranceTestSlotDto.Date.Value;
            }
            await _entranceTestSlotRepository.UpdateAsync(existedSlot);
        }
        public async Task ClearEntranceTestInASlot(long slotId)
        {
            var entranceTestSlot = await GetEntranceTestSlotDetail(slotId);
            foreach (var entranceTest in entranceTestSlot.EntranceTests)
            {
                await _entranceTestService.UpdateEntranceTestId(entranceTest.Id, null);
                var student = await _studentService.GetRequiredStudentById(entranceTest.Student.Id);
                if (student.Status == StudentStatus.EntranceTesting.ToString())
                {
                    await _studentService.ChangeStatusOfStudent(entranceTest.Student.Id, StudentStatus.Accepted.ToString());
                }
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
                    //var student = await _studentService.GetRequiredStudentById(entranceTest.Student.Id);
                    //if (student.Status != StudentStatus.Accepted.ToString())
                    //{
                    //    throw new BadRequestException("Student must have status Accepted to proceed this function");
                    //}
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

        public async Task AnnouceEntranceTestScoreSlot(long slotId)
        {
            var slot = await GetRequiredEntranceTestSlotById(slotId);
            slot.IsAnnoucedScore = true;
            await _entranceTestSlotRepository.UpdateAsync(slot);
        }
        public async Task AnnouceTimeAllEntranceTestSlot()
        {
            var slots = await _entranceTestSlotRepository.FindAsync(s => s.IsAnnoucedTime == false);
            foreach (var slot in slots)
            {
                slot.IsAnnoucedTime = true;
            }
            await _entranceTestSlotRepository.UpdateRangeAsync(slots);
        }
        public async Task AnnouceScoreAllEntranceTestSlot()
        {
            var slots = await _entranceTestSlotRepository.FindAsync(s => s.IsAnnoucedScore == false);
            foreach (var slot in slots)
            {
                slot.IsAnnoucedScore = true;
            }
            await _entranceTestSlotRepository.UpdateRangeAsync(slots);
        }

        public async Task DeleteEntranceTestSlot(long slotId)
        {
            await ClearEntranceTestInASlot(slotId);
            await _entranceTestSlotRepository.DeleteAsync(slotId);
        }
    }
}
