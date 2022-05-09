
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AspireOverflow.Models
{

    public partial class ArticleLike
    {


        [Key]
        public int LikeId { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("ArticleId")]
        [InverseProperty("ArticleLikes")]
        public virtual Article? Article { get; set; }=null!;
    
    [ForeignKey("UserId")]
     [InverseProperty("Likes")]
    public virtual User? LikedUser { get; set; }=null!;
}
}
