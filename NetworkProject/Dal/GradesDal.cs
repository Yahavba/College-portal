using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NetworkProject.Dal
{
    public class GradesDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Grade>().ToTable("Grades");
        }
        public DbSet<Grade> Grades { get; set; }
    }
}