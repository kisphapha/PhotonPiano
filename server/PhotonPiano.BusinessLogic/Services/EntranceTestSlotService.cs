
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
        private readonly IUtilities _utilities;

        public EntranceTestSlotSerivce(IEntranceTestSlotRepository entranceTestSlotRepository, 
            ILocationService locationSerivce, IEntranceTestService entranceTestService, 
            IStudentService studentService, IInstructorService instructorService,
            IUtilities utilities)
        {
            _entranceTestSlotRepository = entranceTestSlotRepository;
            _locationService = locationSerivce;
            _entranceTestService = entranceTestService;
            _studentService = studentService;
            _instructorService = instructorService;
            _utilities = utilities;
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

            if (!await CheckScheduleConflict(mappedEntranceTestSlot.LocationId, mappedEntranceTestSlot.InstructorId,
                mappedEntranceTestSlot.Shift, mappedEntranceTestSlot.Date))
            {
                throw new BadRequestException("Cannot create due to schedule conflict. Please check again!");
            }

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
 
            if (!await CheckScheduleConflict(existedSlot.LocationId, existedSlot.InstructorId, existedSlot.Shift, existedSlot.Date))
            {
                throw new BadRequestException("Cannot update due to schedule conflict. Please check again!");
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

        public async Task AutoArrangeEntranceTests(AutoArrangeSlotDto autoArrangeSlotDto)
        {
            //filter
            if (autoArrangeSlotDto.From > autoArrangeSlotDto.To)
            {
                throw new BadRequestException("From date is not larger than To date");
            }
            if (!autoArrangeSlotDto.AllowedShifts.All(s => s >= 1 && s <= 8))
            {
                throw new BadRequestException("Shifts can only be from range 1 to 8");
            }
            foreach (var locationId in autoArrangeSlotDto.AllowedLocationIds)
            {
                var location = await _locationService.GetLocationById(locationId, true);
                if (location!.Status == LocationStatus.Unavailable.ToString())
                {
                    throw new BadRequestException($"Location {location.Id} is unavailable");
                }
            }
            foreach (var instructorId in autoArrangeSlotDto.AllowedInstructorIds)
            {
                await _instructorService.GetRequiredInstructorById(instructorId);
            }
            //action
            var rand = new Random();
            var students = await _studentService.GetAllAcceptedStudents();
            long acceptingNumber = Math.Min(students.Count, autoArrangeSlotDto.NumberOfStudents);

            int maxTimeOut = 200;
            long capacity = -1, accepted = 0;
            long slotId = 0;
            long randomLocationId, randomInstructorId;
            DateOnly randomDate;
            int randomShift;
            for (var i = 0; i < acceptingNumber; i++)
            {
                if (accepted >= capacity)
                {
                    var timeOut = 0;
                    do
                    {
                        randomLocationId = autoArrangeSlotDto.AllowedLocationIds[rand.Next(autoArrangeSlotDto.AllowedLocationIds.Count)];
                        randomInstructorId = autoArrangeSlotDto.AllowedInstructorIds[rand.Next(autoArrangeSlotDto.AllowedInstructorIds.Count)];
                        randomShift = autoArrangeSlotDto.AllowedShifts[rand.Next(autoArrangeSlotDto.AllowedShifts.Count)];
                        randomDate = _utilities.GetRandomDateBetween(autoArrangeSlotDto.From, autoArrangeSlotDto.To);
                        timeOut++;
                        if (timeOut >= maxTimeOut)
                        {
                            throw new BadRequestException("Unable to continue arranging due to unavoidable conflicts." +
                                "Please extend the range to increase the chance of success!");
                        }
                    }
                    while (!await CheckScheduleConflict(randomLocationId, randomInstructorId, randomShift, randomDate));
                    var createdSlot = await CreateEntranceTestSlot(new CreateEntranceTestSlotDto
                    {
                        Date = randomDate,
                        LocationId = randomLocationId,
                        Shift = randomShift,
                        InstructorId = randomInstructorId
                    });

                    var location = await _locationService.GetLocationById(randomLocationId, true);
                    capacity = location!.Capacity;
                    accepted = 0;
                    slotId = createdSlot.Id;
                }
                var entranceTest = await _entranceTestService.GetUnScoreEntranceTestByStudentId(students[i].Id);
                if (entranceTest != null)
                {
                    await _entranceTestService.UpdateEntranceTestId(entranceTest.Id, slotId);
                    await _studentService.ChangeStatusOfStudent(students[i].Id, StudentStatus.EntranceTesting.ToString());
                    accepted += 1;
                }  
            }
        }

        public async Task<bool> CheckScheduleConflict(long locationId, long? instructorId, int shift, DateOnly date)
        {
            var instructorConflict = await _entranceTestSlotRepository
                .FindOneAsync(s => 
                    s.InstructorId == instructorId && 
                    s.Shift == shift && 
                    s.Date == date);

            var locationConflict = await _entranceTestSlotRepository
                .FindOneAsync(s =>
                    s.LocationId == locationId &&
                    s.Shift == shift &&
                    s.Date == date);

            return instructorConflict is null && locationConflict is null;
        }
    }
}
