using AspireOverflow.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.Models
{
    public partial class Article : IAuditField
    {


        [Key]
        public int ArtileId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public byte[] Image { get; set; }
        public int ArticleStatusID { get; set; }
        public int? ReviewerId { get; set; }


        public DateTime Datetime { get; set; }

        public bool IsPrivate { get; set; } = false;

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        [NotMapped]
        public string ImageString { get; set; }

        [ForeignKey("ArticleStatusID")]
        [InverseProperty("Articles")]
        public virtual ArticleStatus? ArticleStatus { get; set; }



        [ForeignKey("CreatedBy")]
        [InverseProperty("Articles")]
        public virtual User? User { get; set; } = null!;

        [InverseProperty("Article")]
        public virtual ICollection<ArticleComment>? ArticleComments { get; set; }


        [InverseProperty("Article")]
        public virtual ICollection<ArticleLike>? ArticleLikes { get; set; }

        [InverseProperty("article")]
        public virtual ICollection<PrivateArticle>? PrivateArticles { get; set; }

    }
    public partial class PrivateArticle
    {
        public PrivateArticle(int articleId, int userId)
        {
            ArticleId = articleId;
            UserId = userId;
        }

        [Key]
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("ArticleId")]
        [InverseProperty("PrivateArticles")]
        public virtual Article? article { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("PrivateArticles")]
        public virtual User? user { get; set; }


    }

    public partial class PrivateArticleDto {

        public Article article { get; set; }
        public List<int> SharedUsersId { get; set; }
    }



}