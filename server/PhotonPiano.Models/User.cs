using System;
using System.Collections.Generic;

namespace PhotonPiano.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
