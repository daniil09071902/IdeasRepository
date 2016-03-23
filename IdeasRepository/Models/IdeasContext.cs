using IdeasRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace IdeasRepository.Models
{
    public class IdeasContext: DbContext
    {
        public IdeasContext() : base("Connection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
     }
}