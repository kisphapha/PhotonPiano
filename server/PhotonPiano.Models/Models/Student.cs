﻿using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class Student
{
    public long Id { get; set; }

    public DateOnly? JoinedDate { get; set; }

    public long? CurrentClassId { get; set; }

    public int Level { get; set; }

    public string Status { get; set; } = null!;

    public long UserId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string? ShortDesc { get; set; }

    public virtual Class? CurrentClass { get; set; }

    public virtual ICollection<EntranceTest> EntranceTests { get; set; } = new List<EntranceTest>();

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public virtual ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();

    public virtual ICollection<SwitchClassRequest> SwitchClassRequests { get; set; } = new List<SwitchClassRequest>();

    public virtual User User { get; set; } = null!;
}
