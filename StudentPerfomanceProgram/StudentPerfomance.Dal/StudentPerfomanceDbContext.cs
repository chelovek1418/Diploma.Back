using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;

namespace StudentPerfomance.Dal
{
    public class StudentPerfomanceDbContext : DbContext
    {
        public StudentPerfomanceDbContext(DbContextOptions<StudentPerfomanceDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupSubject> GroupSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<Detail> Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ_User_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("UQ_User_PhoneNumber")
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

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(30);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Student_GroupId");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Id)
                    .HasConstraintName("FK_Student_Id");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasQueryFilter(u => u.IsConfirmed);

                entity.Property(e => e.IsConfirmed)
                    .HasDefaultValue(false);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.Id)
                    .HasConstraintName("FK_Teacher_Id");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("UQ_Group_Title")
                    .IsUnique();

                entity.Property(e => e.Faculty)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Speciality)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Specialization)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasCheckConstraint("CK_Group_Year_More_Than_0", "[Year] >= 1 AND [Year] <= 6");
            });

            modelBuilder.Entity<GroupSubject>(entity =>
            {
                entity.HasIndex(e => e.SubjectId);

                entity.HasIndex(e => e.GroupId);

                entity.HasAlternateKey(e => new { e.GroupId, e.SubjectId });

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupSubjects)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupSubject_GroupId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.GroupSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_GroupSubject_SubjectId");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasIndex(e => e.SubjectId);

                entity.HasIndex(e => e.StudentId);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Mark_SubjectId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Mark_StudentId");
            });

            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.SubjectId })
                    .HasName("PK_TeacherSubject_TeacherIdSubjectId");

                entity.HasIndex(e => e.SubjectId);

                entity.HasIndex(e => e.TeacherId);

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TeacherSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_TeacherSubject_SubjectId");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherSubjects)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_TeacherSubject_TeacherId");
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasOne(d => d.GroupSubject)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.GroupSubjectId)
                    .HasConstraintName("FK_Detail_GroupSubjectId");

                entity.Property(e => e.Semestr)
                    .HasColumnType("tinyint")
                    .HasDefaultValue(0);
                
                entity.HasCheckConstraint("CK_Detail_Semestr", "[Semestr] >= 0 AND [Semestr] <= 3");

                entity.HasCheckConstraint("CK_Detail_DayOfWeek", "[DayOfWeek] >= 0 AND [DayOfWeek] <= 6");

                entity.HasCheckConstraint("CK_Detail_Pair", "[Pair] >= 0 AND [Pair] <= 4");
            });
        }
    }
}
