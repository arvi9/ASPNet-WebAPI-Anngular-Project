

using AspireOverflow.Models;
using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.DataAccessLayer
{
    public class AspireOverflowContext : DbContext
    {


        public AspireOverflowContext(DbContextOptions<AspireOverflowContext> options) : base(options)
        {
              
         }



        public DbSet<Query> Queries { get; set; }
        public DbSet<QueryComment> QueryComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            {
                modelBuilder.Entity<QueryComment>(entity =>
              {


                  entity.HasOne(d => d.Query)
                      .WithMany(p => p.QueryComments)
                      .HasForeignKey(d => d.QueryId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_QueryComment_Query");


              });
            }
        }
    }
}