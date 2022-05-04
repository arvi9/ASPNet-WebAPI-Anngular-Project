
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AspireOverflow.Models
{

    public partial class ArticleStatus
    {


        [Key]
        public int ArticleStatusID { get; set; }
        public string Status { get; set; }


        [InverseProperty("ArticleStatus")]
        public virtual ICollection<Article> Articles { get; set; }
    }
}
