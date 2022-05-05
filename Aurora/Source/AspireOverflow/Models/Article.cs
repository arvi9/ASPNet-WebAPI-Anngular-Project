using AspireOverflow.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.Models
{
    public partial class Article : IAuditField
    {

        /*public Article()
        {
            ArticleComments = new HashSet<ArticleComment>();
           // ArticleLikes = new HashSet<ArticleLike>();
        }*/

        [Key]
        public int ArtileId { get; set; }

        public string Title {get; set;}

        public string Content { get; set; }

        public byte[] Image { get ; set; }
        public int ArticleStatusID { get; set; }
        public int? ReviewerId {get; set; }
   

        public DateTime Datetime { get; set; }

        public int CreatedBy { get; set; }
       
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        
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
        
    }
}