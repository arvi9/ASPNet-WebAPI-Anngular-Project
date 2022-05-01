

using AspireOverflow.Models;
using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.DataAccessLayer
{
    public class AspireOverflowContext : DbContext
    {


        public AspireOverflowContext(DbContextOptions<AspireOverflowContext> options) : base(options)
        {


        }


        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<Query> Queries { get; set; } = null!;
        public DbSet<QueryComment> QueryComments { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Designation> Designations { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Query>(entity =>
                       {

                           entity.HasOne(d => d.User)
                               .WithMany(p => p.Queries)
                               .HasForeignKey(d => d.CreatedBy)
                               .OnDelete(DeleteBehavior.ClientSetNull)
                               .HasConstraintName("FK_Query_User");
                       });

            modelBuilder.Entity<QueryComment>(entity =>
            {



                entity.HasOne(d => d.Query)
                    .WithMany(p => p.QueryComments)
                    .HasForeignKey(d => d.QueryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QueryComment_Query");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QueryComments)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QueryComment_User");
            });

        }
    }
}