using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Views;

namespace StudentPerfomance.Dal.Data
{
    public partial class StudentPerfomanceDbContext : IdentityDbContext<Users, UserRole, int>
    {
        public StudentPerfomanceDbContext() { }

        public StudentPerfomanceDbContext(DbContextOptions<StudentPerfomanceDbContext> options) : base(options) { }

        public virtual DbSet<AverageStudentGrade> AverageStudentGrade { get; set; }
        public virtual DbSet<GetBestStudentIdView> GetBestStudentIdView { get; set; }
        public virtual DbSet<GetWorstStudentIdView> GetWorstStudentIdView { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<GroupsLessons> GroupsLessons { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<Marks> Marks { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<UsersLessons> UsersLessons { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudentPerfomanceDb;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AverageStudentGrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToView(nameof(AverageStudentGrade));
            });

            modelBuilder.Entity<GetBestStudentIdView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView(nameof(GetBestStudentIdView));

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GetWorstStudentIdView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView(nameof(GetWorstStudentIdView));

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
                entity.Property(e => e.MarkDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

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
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Student_GroupId");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Students)
                    .HasForeignKey<Students>(d => d.Id)
                    .HasConstraintName("FK_Student_Id");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ_User_Email")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

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

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).HasMaxLength(1);
            });

            modelBuilder.Entity<UsersLessons>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LessonId })
                    .HasName("PK_UserLesson_UserIdLessonId");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

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
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
