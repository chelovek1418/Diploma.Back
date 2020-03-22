using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Views;

namespace StudentPerfomance.Dal
{
    public partial class StudentPerfomanceDbContext : DbContext
    {
        public StudentPerfomanceDbContext()
        {
        }

        public StudentPerfomanceDbContext(DbContextOptions<StudentPerfomanceDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AverageStudentGrade> AverageStudentGrade { get; set; }
        public virtual DbSet<GetBestStudentIdView> GetBestStudentIdView { get; set; }
        public virtual DbSet<GetWorstStudentIdView> GetWorstStudentIdView { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<RatingByLesson> Ratings { get; set; }
        public virtual DbSet<GroupsLessons> GroupsLessons { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<Marks> Marks { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UsersLessons> UsersLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AverageStudentGrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AverageStudentGrade");
            });

            modelBuilder.Entity<GetBestStudentIdView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetBestStudentIdView");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GetWorstStudentIdView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetWorstStudentIdView");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("UQ_Group_Title")
                    .IsUnique();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<GroupsLessons>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.LessonId })
                    .HasName("PK_GroupLesson_GroupIdLessonId");

                entity.HasIndex(e => e.LessonId);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupsLessons)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupLesson_UserId");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.GroupsLessons)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK_GroupLesson_LessonId");
            });

            modelBuilder.Entity<Lessons>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Marks>(entity =>
            {
                entity.HasIndex(e => e.LessonId);

                entity.HasIndex(e => e.StudentId);

                entity.Property(e => e.MarkDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK_Mark_LessonId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Mark_StudentId");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasIndex(e => e.GroupId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Student_GroupId");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Students)
                    .HasForeignKey<Students>(d => d.Id)
                    .HasConstraintName("FK_Student_Id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ_User_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Photo).HasMaxLength(1);
            });

            modelBuilder.Entity<UsersLessons>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LessonId })
                    .HasName("PK_UserLesson_UserIdLessonId");

                entity.HasIndex(e => e.LessonId);

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.UsersLessons)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK_UserLesson_LessonId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersLessons)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserLesson_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
