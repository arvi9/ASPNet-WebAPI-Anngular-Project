using AspireOverflow.Models.Interfaces;
namespace AspireOverflow.Models
{
    public class Query :IAuditField
    {
        public Query(){
            QueryComments = new HashSet<QueryComment>();
        }
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string code { get; set; }

        public bool IsSolved { get; set; }

      
 
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        


      public ICollection<QueryComment>? QueryComments {get;set;}
    }
}