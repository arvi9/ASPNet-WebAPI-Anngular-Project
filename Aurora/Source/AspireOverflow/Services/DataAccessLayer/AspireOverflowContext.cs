

using AspireOverflow.Models;
using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.DataAccessLayer
{
    public class AspireOverflowContext : DbContext
    {

        public AspireOverflowContext() { }
        public AspireOverflowContext(DbContextOptions<AspireOverflowContext> options) : base(options)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (!optionsBuilder.IsConfigured)
                    optionsBuilder.UseSqlServer("Server=.;database=AspireOverflow;Trusted_Connection=true;");


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public DbSet<Query> Queries { get; set; }
        public DbSet<QueryComment> QueryComments { get; set; }


    }
}