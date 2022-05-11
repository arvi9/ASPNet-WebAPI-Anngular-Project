using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspireOverflow.Models
{
    public class Spam
    {
        [Key]
        public int SpamId { get; set; }

        public string Reason { get; set; }
        public int QueryId { get; set; }
        public int UserId { get; set; }

        public int VerifyStatusID { get; set; }

        [ForeignKey("VerifyStatusID")]
        public virtual VerifyStatus? VerifyStatus { get; set; }
        [ForeignKey("QueryId")]
        public virtual Query? Query { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }



    }
}