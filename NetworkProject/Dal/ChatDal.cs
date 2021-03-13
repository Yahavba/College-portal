using NetworkProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NetworkProject.Dal
{
    public class ChatDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>().ToTable("Chat");
        }
        public DbSet<Message> messages { get; set; }
    }
}