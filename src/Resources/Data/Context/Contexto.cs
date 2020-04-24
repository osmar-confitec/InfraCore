using Microsoft.EntityFrameworkCore;
using Resources.Data.Mapping;
using Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Data.Context
{
   public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options)
       : base(options)
        {
        
        
        }
        public DbSet<Resources<string>> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ResourcesMapping());
        }

    }
}
