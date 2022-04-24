
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;


namespace AspireOverflow.Models
{
   public class QueryComment
    {
        [Key]
        public int Id { get; set; }

        public string Comment { get; set; }

        public DateTime Datetime { get; set; }

        public int QueryId { get; set; }
        public int UserId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        [ForeignKey("QueryId")]
        public virtual Query Query { get; set; }



    }
}