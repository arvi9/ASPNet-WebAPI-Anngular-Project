
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

using AspireOverflow.Models.Interfaces;

namespace AspireOverflow.Models
{
   public partial class QueryComment :IAuditField
    {
        public QueryComment(){
    
        }
        [Key]
        public int QueryCommentId { get; set; }

        public string Comment { get; set; }

        public DateTime Datetime { get; set; }

       public string? Code { get; set; }
        public int QueryId { get; set; }
       
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        
       [ForeignKey("QueryId")]
        [InverseProperty("QueryComments")]
        public virtual Query? Query { get; set; }

         [ForeignKey("CreatedBy")]
      [InverseProperty("QueryComments")]
      public virtual User? User {get;set;}

}}