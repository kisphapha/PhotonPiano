using System;
using System.Collections.Generic;

namespace PhotonPiano.Models;

public partial class Lesson
{
    public long Id { get; set; }

    public int Shift { get; set; }

    public long LocationId { get; set; }

    public long? ClassId { get; set; }

    public bool IsExam { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
}
