using Microsoft.EntityFrameworkCore;
using StudentSystemDatabase.Data;
using StudentSystemDatabase.Data.Enums;
using StudentSystemDatabase.Data.Models;

namespace StudentSystemDatabase.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext() {}

        public StudentSystemContext(DbContextOptions options) 
        : base(options) {}


        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentsCourses{ get; set; }
        public DbSet<Resource> Resources{ get; set; }
        public DbSet<HomeworkSubmission> HomeworkSubmissions{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.Name)
                .IsUnicode(true)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                .IsUnicode(false)
                .IsRequired(false)
                .HasMaxLength(10);

                entity.Property(e => e.RegisteredOn)
                .IsRequired();

                entity.Property(e => e.BirthDate)
                .IsRequired(false);

            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(80);

                entity.Property(e => e.Description)
                .IsUnicode(true)
                .IsRequired(false);

                entity.Property(e => e.StartDate)
                .IsRequired(true);

                entity.Property(e => e.EndDate)
                .IsRequired(true);

                entity.Property(e => e.Price)
                .IsRequired(true)
                .HasColumnType("DECIMAL")
                .HasPrecision(2);

                entity.HasMany(e => e.HomeworkSubmissions)
                .WithOne(hw => hw.Course)
                .HasForeignKey(hw => hw.CourseId);

                entity.HasMany(e => e.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(e => e.CourseId);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new {e.StudentId, e.CourseId});

                entity.HasOne(e => e.Student)
                .WithMany(s => s.StudentsCourses)
                .HasForeignKey(e => e.StudentId)
                .HasConstraintName("FK_StudentsCourses_Student");

                entity.HasOne(e => e.Course)
               .WithMany(s => s.StudentsCourses)
               .HasForeignKey(e => e.CourseId)
               .HasConstraintName("FK_StudentsCourses_Course");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.ResourceId);

                entity.Property(e => e.Name)
                .IsUnicode(true)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(e => e.Url)
                .IsUnicode(false)
                .IsRequired();

                entity.Property(e => e.ResourceType)
                .HasDefaultValue(ResourceType.Other);
            });

            modelBuilder.Entity<HomeworkSubmission>(entity =>
            {
                entity.HasKey(e => e.HomeworkId);

                entity.Property(e => e.Content)
                .IsUnicode(false);

                entity.Property(e => e.SubmissionTime)
                .IsRequired();

                entity.Property(e => e.StudentId)
                .IsRequired();

                entity.Property(e => e.CourseId)
                .IsRequired();

                
            });

        }
    }
}