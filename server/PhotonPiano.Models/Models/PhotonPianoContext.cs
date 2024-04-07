using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PhotonPiano.Models.Models;

public partial class PhotonPianoContext : DbContext
{
    public PhotonPianoContext()
    {
    }

    public PhotonPianoContext(DbContextOptions<PhotonPianoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentVote> CommentVotes { get; set; }

    public virtual DbSet<ConfigurationVarible> ConfigurationVaribles { get; set; }

    public virtual DbSet<Criterion> Criteria { get; set; }

    public virtual DbSet<EntranceTest> EntranceTests { get; set; }

    public virtual DbSet<EntranceTestResult> EntranceTestResults { get; set; }

    public virtual DbSet<EntranceTestSlot> EntranceTestSlots { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostVote> PostVotes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentClass> StudentClasses { get; set; }

    public virtual DbSet<StudentClassScore> StudentClassScores { get; set; }

    public virtual DbSet<StudentClassTuition> StudentClassTuitions { get; set; }

    public virtual DbSet<StudentLesson> StudentLessons { get; set; }

    public virtual DbSet<SwitchClassRequest> SwitchClassRequests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public static string? GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string? connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("PhotonPiano"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("class_id_primary");

            entity.ToTable("Class");

            entity.HasIndex(e => e.InstructorId, "IX_Class_Instructor_Id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.InstructorId).HasColumnName("Instructor_Id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Classes)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_Instructor");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_id_primary");

            entity.ToTable("Comment");

            entity.HasIndex(e => e.PostId, "IX_Comment_Post_id");

            entity.HasIndex(e => e.ReplyToCommentId, "IX_Comment_ReplyToCommentId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentDate)
                .HasColumnType("datetime")
                .HasColumnName("Comment_date");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.PostId).HasColumnName("Post_id");

            entity.HasOne(d => d.Owner).WithMany(p => p.Comments)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_User");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Post");

            entity.HasOne(d => d.ReplyToComment).WithMany(p => p.InverseReplyToComment)
                .HasForeignKey(d => d.ReplyToCommentId)
                .HasConstraintName("FK_Comment_Comment");
        });

        modelBuilder.Entity<CommentVote>(entity =>
        {
            entity.HasKey(e => new { e.CommentId, e.UserId });

            entity.ToTable("CommentVote");

            entity.HasIndex(e => e.UserId, "IX_CommentVote_UserId");

            entity.Property(e => e.UpOrDown).HasColumnName("Up_or_down");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentVotes)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommentVote_Comment");

            entity.HasOne(d => d.User).WithMany(p => p.CommentVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommentVote_User");
        });

        modelBuilder.Entity<ConfigurationVarible>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(200);
        });

        modelBuilder.Entity<Criterion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("criteria_id_primary");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.For).HasMaxLength(255);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<EntranceTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EntranceTest_");

            entity.ToTable("EntranceTest");

            entity.HasIndex(e => e.StudentId, "IX_EntranceTest_StudentId");

            entity.Property(e => e.BandScore).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.EntranceTestSlot).HasColumnName("Entrance_test_slot");
            entity.Property(e => e.Rank)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.EntranceTestSlotNavigation).WithMany(p => p.EntranceTests)
                .HasForeignKey(d => d.EntranceTestSlot)
                .HasConstraintName("FK_EntranceTest_EntranceTestSlot");

            entity.HasOne(d => d.Student).WithMany(p => p.EntranceTests)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_EntranceTest_Student1");
        });

        modelBuilder.Entity<EntranceTestResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entrancetest_id_primary");

            entity.ToTable("EntranceTestResult");

            entity.HasIndex(e => e.CriteriaId, "IX_EntranceTestResult_Criteria_id");

            entity.HasIndex(e => e.EntranceTestId, "IX_EntranceTestResult_entrance_test_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CriteriaId).HasColumnName("Criteria_id");
            entity.Property(e => e.EntranceTestId).HasColumnName("entrance_test_id");
            entity.Property(e => e.Score).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Criteria).WithMany(p => p.EntranceTestResults)
                .HasForeignKey(d => d.CriteriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntranceTest_Criteria");

            entity.HasOne(d => d.EntranceTest).WithMany(p => p.EntranceTestResults)
                .HasForeignKey(d => d.EntranceTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntranceTestResult_EntranceTest");
        });

        modelBuilder.Entity<EntranceTestSlot>(entity =>
        {
            entity.ToTable("EntranceTestSlot");

            entity.HasOne(d => d.Location).WithMany(p => p.EntranceTestSlots)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntranceTestSlot_Location");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instruct__3213E83F976F1BA2");

            entity.ToTable("Instructor");

            entity.HasIndex(e => e.UserId, "IX_Instructor_UserId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContributeScore).HasColumnName("Contribute_score");

            entity.HasOne(d => d.User).WithMany(p => p.Instructors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Instructor_User");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lesson_id_primary");

            entity.ToTable("Lesson");

            entity.HasIndex(e => e.ClassId, "IX_Lesson_ClassId");

            entity.HasIndex(e => e.LocationId, "IX_Lesson_locationId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExamType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LocationId).HasColumnName("locationId");
            entity.Property(e => e.Shift).HasColumnName("shift");

            entity.HasOne(d => d.Class).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Lesson_Class");

            entity.HasOne(d => d.Location).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_Location");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("location_id_primary");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.HasIndex(e => e.RecieverId, "IX_Notification_RecieverId");

            entity.Property(e => e.Picture).IsUnicode(false);
            entity.Property(e => e.RefUrl).IsUnicode(false);
            entity.Property(e => e.ViewStatus).HasColumnName("View_status");

            entity.HasOne(d => d.Reciever).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.RecieverId)
                .HasConstraintName("FK_Notification_User");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_id_primary");

            entity.ToTable("Post");

            entity.HasIndex(e => e.OwnerId, "IX_Post_ownerId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Downvote).HasColumnName("downvote");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.PostedDate)
                .HasColumnType("datetime")
                .HasColumnName("posted_date");
            entity.Property(e => e.Upvote).HasColumnName("upvote");

            entity.HasOne(d => d.Owner).WithMany(p => p.Posts)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_User");
        });

        modelBuilder.Entity<PostVote>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.UserId });

            entity.ToTable("PostVote");

            entity.HasIndex(e => e.UserId, "IX_PostVote_UserId");

            entity.Property(e => e.UpOrDown).HasColumnName("Up_or_down");

            entity.HasOne(d => d.Post).WithMany(p => p.PostVotes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostVote_Post");

            entity.HasOne(d => d.User).WithMany(p => p.PostVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostVote_User");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("student_id_primary");

            entity.ToTable("Student");

            entity.HasIndex(e => e.CurrentClassId, "IX_Student_CurrentClassId");

            entity.HasIndex(e => e.UserId, "IX_Student_UserId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.ShortDesc)
                .HasMaxLength(500)
                .HasColumnName("Short_desc");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CurrentClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.CurrentClassId)
                .HasConstraintName("FK_Student_Class");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_User");
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("studentclass_id_primary");

            entity.ToTable("StudentClass");

            entity.HasIndex(e => e.ClassId, "IX_StudentClass_classId");

            entity.HasIndex(e => e.StudentId, "IX_StudentClass_studentId");

            entity.Property(e => e.ClassId).HasColumnName("classId");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("GPA");
            entity.Property(e => e.InstructorComment).HasColumnName("Instructor_comment");
            entity.Property(e => e.Rank)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Result)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StudentId).HasColumnName("studentId");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClass_Class");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClass_Student");
        });

        modelBuilder.Entity<StudentClassScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("studentclassscore_id_primary");

            entity.ToTable("StudentClassScore");

            entity.HasIndex(e => e.CriteriaId, "IX_StudentClassScore_criteriaId");

            entity.HasIndex(e => e.StudentClassId, "IX_StudentClassScore_student_class_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CriteriaId).HasColumnName("criteriaId");
            entity.Property(e => e.Score)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("score");
            entity.Property(e => e.StudentClassId).HasColumnName("student_class_id");

            entity.HasOne(d => d.Criteria).WithMany(p => p.StudentClassScores)
                .HasForeignKey(d => d.CriteriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClassScore_Criteria");

            entity.HasOne(d => d.StudentClass).WithMany(p => p.StudentClassScores)
                .HasForeignKey(d => d.StudentClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClassScore_StudentClass");
        });

        modelBuilder.Entity<StudentClassTuition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("studentclasstuition_id_primary");

            entity.ToTable("StudentClassTuition");

            entity.HasIndex(e => e.StudentClassId, "IX_StudentClassTuition_student_class_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasColumnName("due_date");
            entity.Property(e => e.Month).HasColumnName("month");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StudentClassId).HasColumnName("student_class_id");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("transaction_description");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.StudentClass).WithMany(p => p.StudentClassTuitions)
                .HasForeignKey(d => d.StudentClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClassTuition_StudentClass");
        });

        modelBuilder.Entity<StudentLesson>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.LessonId }).HasName("PK__StudentL__D2997F4B71CCF9B2");

            entity.ToTable("StudentLesson");

            entity.HasIndex(e => e.LessonId, "IX_StudentLesson_lessonId");

            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.LessonId).HasColumnName("lessonId");

            entity.HasOne(d => d.Lesson).WithMany(p => p.StudentLessons)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentLesson_Lesson");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentLessons)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentLesson_Student");
        });

        modelBuilder.Entity<SwitchClassRequest>(entity =>
        {
            entity.ToTable("SwitchClassRequest");

            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.RequestDate).HasColumnType("datetime");

            entity.HasOne(d => d.OldClass).WithMany(p => p.SwitchClassRequests)
                .HasForeignKey(d => d.OldClassId)
                .HasConstraintName("FK_SwitchClassRequest_Class");

            entity.HasOne(d => d.Student).WithMany(p => p.SwitchClassRequests)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_SwitchClassRequest_Student");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_id_primary");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.BankAccount)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
