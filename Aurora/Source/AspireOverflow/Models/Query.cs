using AspireOverflow.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspireOverflow.Models
{
    public partial class Query :IAuditField
    {
        public Query(){
            QueryComments = new HashSet<QueryComment>();
        }
        [Key]
        public int QueryId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string code { get; set; }

        public bool IsSolved { get; set; }

      public bool IsActive{get;set;} =true;
 
      
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        

        [InverseProperty("Query")]
      public virtual ICollection<QueryComment>? QueryComments {get;set;}

      [ForeignKey("CreatedBy")]
      [InverseProperty("Queries")]
      public virtual User? User {get;set;}
    }
}