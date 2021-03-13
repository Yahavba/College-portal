using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NetworkProject.Models;
using System.Web;

namespace NetworkProject.Models
{
    public class CoursesDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().ToTable("Courses");
        }
        public DbSet<Course> Courses { get; set; }
    }
}