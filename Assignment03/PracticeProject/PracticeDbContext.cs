using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticeProject;
using PracticeProject.Seeds;

namespace PracticeProject
{

    public class PracticeDBContext : DbContext
    {
        private readonly string _connectionString;
        public PracticeDBContext()
        {
            _connectionString = "Server=DESKTOP-74J889G\\SQLEXPRESS;Database=AttendanceSystem;User Id=PracticeLogin;Password=password;TrustServerCertificate=True";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(AdminSeed.Admin);

            base.OnModelCreating(modelBuilder);
        }
        
        
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
