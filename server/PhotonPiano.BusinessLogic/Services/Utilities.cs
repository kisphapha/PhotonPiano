using PhotonPiano.BusinessLogic.Interfaces;

namespace PhotonPiano.BusinessLogic.Services
{
    public class Utilities : IUtilities
    {
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
    }
}
