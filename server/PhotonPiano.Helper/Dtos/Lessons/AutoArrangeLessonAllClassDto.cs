

using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Lessons
{
    public class AutoArrangeLessonAllClassDto
    {
        [Required]
        [Range(1, 56, ErrorMessage = "Lesson each week must be an integer between 1 and 56.")]
        public int LessonEachWeek { get; set; }
        [Required]
        public DateOnly StartingFrom { get; set; }
        [Required]
        public int TotalWeeks { get; set; }
        [Required]
        public List<long> AllowedLocationIds { get; set; } = new List<long>();
        [Required]
        public List<int> AllowedShift { get; set; } = new List<int>();
        public List<DateOnly> DayOffs { get; set; } = new List<DateOnly>();

        [Required]
        public ScheduleClassesOption ClassesOption { get; set; }
        public ScheduleHandleLessonOption? HandleLessonOption { get; set; }
        //Options
        public bool OptionShiftConsistency { get; set; } = false;
        public bool OptionLocationConsistency { get; set; } = false;
        public bool OptionLocationSimilar { get; set; } = false;
        public bool OptionIncludeSaturday { get; set; } = false;
        public bool OptionIncludeSunday { get; set; } = false;

    }
}
