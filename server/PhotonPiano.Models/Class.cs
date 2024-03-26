﻿using System;
using System.Collections.Generic;

namespace PhotonPiano.Models;

public partial class Class
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int Level { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Status { get; set; } = null!;

    public long InstructorId { get; set; }

    public virtual Instructor Instructor { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
}
