using System;
using System.Collections.Generic;

namespace PhotonPiano.Models;

public partial class StudentClass
{
    public long Id { get; set; }

    public long StudentId { get; set; }

    public long ClassId { get; set; }

    public string? Result { get; set; }

    public decimal? Gpa { get; set; }

    public string? Rank { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<StudentClassScore> StudentClassScores { get; set; } = new List<StudentClassScore>();

    public virtual ICollection<StudentClassTuition> StudentClassTuitions { get; set; } = new List<StudentClassTuition>();
}
