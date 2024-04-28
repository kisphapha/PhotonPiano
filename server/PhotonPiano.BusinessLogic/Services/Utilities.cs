using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Ultilities;

namespace PhotonPiano.BusinessLogic.Services
{
    public class Utilities : IUtilities
    {
        //This service is ABSOLUTELY not injecting other services


        public DateOnly GetRandomDateBetween(DateOnly startDate, DateOnly endDate)
        {
            // Create an instance of the Random class
            Random random = new Random();

            // Calculate the total number of days between the start and end dates
            int totalDays = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days + 1;

            // Generate a random number of days within the range
            int randomDays = random.Next(totalDays);

            // Add the random number of days to the start date to get the random date
            DateOnly randomDate = startDate.AddDays(randomDays);

            return randomDate;
        }

        public List<WeekDto> GetAllWeeksInYear(int year)
        {
            List<WeekDto> weeks = new List<WeekDto>();

            DateOnly startDate = new DateOnly(year, 1, 1);
            DateOnly endDate = startDate.AddDays(6 - (int)startDate.DayOfWeek);

            while (startDate.Year == year)
            {
                WeekDto week = new WeekDto
                {
                    StartDate = startDate,
                    EndDate = endDate
                };

                weeks.Add(week);

                startDate = endDate.AddDays(1);
                endDate = startDate.AddDays(6 - (int)startDate.DayOfWeek);
            }

            return weeks;
        }
    }
}
