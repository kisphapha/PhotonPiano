using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class Lesson
{
    public long Id { get; set; }

    public int Shift { get; set; }

    public long LocationId { get; set; }

    public long ClassId { get; set; }

    public string? ExamType { get; set; }

    public DateOnly Date { get; set; }

    public bool IsLocked { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
}
