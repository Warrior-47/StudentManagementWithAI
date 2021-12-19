using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementWithAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Data {
    public class ApplicationDbContext : IdentityDbContext {

        public DbSet<Department> Department { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CoursesOffered> CoursesOffered { get; set; }
        public DbSet<CourseTaken> CourseTaken { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CoursesOffered>()
                .HasKey(c => new { c.FacultyId, c.CourseId, c.Section });

            modelBuilder.Entity<CourseTaken>()
                .HasKey(c => new { c.StudentId, c.FacultyId, c.CourseId, c.Section });
        }
    }
}
