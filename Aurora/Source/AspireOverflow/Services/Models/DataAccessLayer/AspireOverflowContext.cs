
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.Models
{
    public class AspireOverflowContext : DbContext
    {

        public AspireOverflowContext() { }
        public AspireOverflowContext(DbContextOptions<AspireOverflowContext> options) : base(options)
        {

        }

        
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        optionsBuilder.UseSqlServer("Server=.;database=AspireOverflowDB;Trusted_Connection=true;");
    }
        public DbSet<Query> Queries { get; set; }
        public DbSet<QueryComment> QueryComments { get; set; }


    }
}