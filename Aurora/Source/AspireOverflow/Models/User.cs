

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace AspireOverflow.Models
{

    public partial class User
    {
        public User()
        {

        }


        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        public string FullName { get; set; } = null!;


        public int GenderId { get; set; }

        [StringLength(30)]
        public string AceNumber { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        [Column(TypeName = "nvarchar(max)")]
        public string Password { get; set; } = null!;


        public DateTime DateOfBirth { get; set; }

        public int VerifyStatusID { get; set; } = 3;

        public bool IsReviewer { get; set; }

        public int UserRoleId { get; set; } = 2;


        public int DesignationId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;


        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }


        [ForeignKey("DesignationId")]
        [InverseProperty("Users")]

        public virtual Designation? Designation { get; set; } = null!;
        [ForeignKey("GenderId")]
        [InverseProperty("Users")]
        public virtual Gender? Gender { get; set; } = null!;

        [ForeignKey("UserRoleId")]
        [InverseProperty("Users")]
        public virtual UserRole? UserRole { get; set; } = null!;

        [ForeignKey("VerifyStatusID")]
        [InverseProperty("Users")]
        public virtual VerifyStatus? VerifyStatus { get; set; } = null!;

        [InverseProperty("User")]
        public virtual ICollection<Query>? Queries { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<QueryComment>? QueryComments { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<ArticleComment>? ArticleComments { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Article>? Articles { get; set; }
        [InverseProperty("LikedUser")]
        public virtual ICollection<ArticleLike>? Likes { get; set; }
          [InverseProperty("user")]
        public virtual ICollection<PrivateArticle>? PrivateArticles { get; set; }




    }

    public class Login{
        public string  Email { get; set; }
        public string Password { get; set; }
    }

    public class CurrentUser{
           public string? Email { get; set; }

           public int UserId { get; set; }

           public int RoleId { get; set; }

           public bool IsReviewer { get; set; }
    }
}
