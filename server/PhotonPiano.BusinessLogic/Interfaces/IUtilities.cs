
using PhotonPiano.Helper.Dtos.Ultilities;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IUtilities
    {
        DateOnly GetRandomDateBetween(DateOnly startDate, DateOnly endDate);

        List<WeekDto> GetAllWeeksInYear(int year);
    }
}
