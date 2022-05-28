
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
     public virtual DbSet<Spam> Spams { get; set; } = null!;
        public virtual DbSet<PrivateArticle> PrivateArticles { get; set; } = null!;
        public virtual DbSet<ArticleLike> ArticleLikes { get; set; } = null!;
          

   public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<ArticleComment> ArticleComments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        modelBuilder.Entity<Gender>(entity =>
                      {
                          entity.HasData(new Gender { GenderId = 1, Name = "Male" });
                          entity.HasData(new Gender { GenderId = 2, Name = "Female" });
                      });

            modelBuilder.Entity<VerifyStatus>(entity =>
            {
                entity.HasData(new VerifyStatus { VerifyStatusID = 1, Name = "Approved" });
                entity.HasData(new VerifyStatus { VerifyStatusID = 2, Name = "Rejected" });
                entity.HasData(new VerifyStatus { VerifyStatusID = 3, Name = "NotVerified" });
            });

            modelBuilder.Entity<UserRole>(entity =>
           {
               entity.HasData(new UserRole { UserRoleId = 1, RoleName = "Admin" });
               entity.HasData(new UserRole { UserRoleId = 2, RoleName = "User" });

           });
             modelBuilder.Entity<ArticleStatus>(entity =>
            {
                entity.HasData(new ArticleStatus { ArticleStatusID = 1, Status = "InDraft" });
                entity.HasData(new ArticleStatus { ArticleStatusID = 2, Status = "To Be Reviewed" });
                entity.HasData(new ArticleStatus { ArticleStatusID = 3, Status = "Under Review" });
                 entity.HasData(new ArticleStatus { ArticleStatusID = 4, Status = "Published" });
            });

        }
    }
}