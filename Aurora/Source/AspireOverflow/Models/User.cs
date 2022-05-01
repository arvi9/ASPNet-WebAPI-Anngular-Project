
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspireOverflow.Models.Interfaces;


namespace AspireOverflow.Models
{
   
    public partial class User :IAuditField
    {
        public User()
        {

            Queries = new HashSet<Query>();
            QueryComments = new HashSet<QueryComment>();

        }


        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        public string FullName { get; set; } = null!;


        public int GenderId { get; set; }

        [StringLength(30)]
        public string AceNumber { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        [StringLength(50)]
        public string Password { get; set; } = null!;


        public DateTime DateOfBirth { get; set; }

        public bool IsVerified { get; set; }


        public int UserRoleId { get; set; }


        public int DepartmentId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Users")]

        public virtual Department Department { get; set; } = null!;
        [ForeignKey("GenderId")]
        [InverseProperty("Users")]
        public virtual Gender Gender { get; set; } = null!;

        [ForeignKey("UserRoleId")]
        [InverseProperty("Users")]
        public virtual UserRole UserRole { get; set; } = null!;

        [InverseProperty("User")]
        public virtual ICollection<Query> Queries { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<QueryComment> QueryComments { get; set; }
       

    }
}
