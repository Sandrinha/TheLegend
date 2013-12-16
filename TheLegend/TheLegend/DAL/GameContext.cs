using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TheLegend.Models;

namespace TheLegend.DAL
{
    public class GameContext: DbContext
    {
        public DbSet <User> Users { get; set; }
        public DbSet<Registo> Registo { get; set; }
        public DbSet<Mission> Missions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}