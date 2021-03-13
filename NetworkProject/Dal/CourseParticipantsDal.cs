using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NetworkProject.Dal
{
    public class CourseParticipantsDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseParticipant>().ToTable("CourseParticipants");
        }
        public DbSet<CourseParticipant> CourseParticipants { get; set; }
    }
}