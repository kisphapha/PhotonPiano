﻿using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class StudentLesson
{
    public long StudentId { get; set; }

    public long LessonId { get; set; }

    public string Attendence { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
