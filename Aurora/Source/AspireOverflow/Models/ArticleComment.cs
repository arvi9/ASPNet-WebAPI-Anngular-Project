
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

using AspireOverflow.Models.Interfaces;

namespace AspireOverflow.Models
{
    public partial class ArticleComment : IAuditField
    {
        public ArticleComment()
        {

        }

        [Key]
        public int ArticleCommentId { get; set; }

        public string Comment { get; set; }

        public DateTime Datetime { get; set; }


        public int UserId { get;  set;}
        public int CreatedBy{get;set;}
        
      

        public int ArticleId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("ArticleId")]
        [InverseProperty("ArticleComments")]
        public virtual Article? Article { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("ArticleComments")]
        public virtual User? User { get; set; } = null!;
    }
}