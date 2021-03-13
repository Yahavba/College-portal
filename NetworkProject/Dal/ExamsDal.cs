using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NetworkProject.Dal
{
    public class ExamsDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exam>().ToTable("Exams");
        }
        public DbSet<Exam> Exams { get; set; }
    }
}