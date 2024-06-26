﻿using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string Role { get; set; } = null!;

    public string? Picture { get; set; }

    public DateOnly? DoB { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public string? BankAccount { get; set; }

    public virtual ICollection<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PostVote> PostVotes { get; set; } = new List<PostVote>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
