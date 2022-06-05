
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AspireOverflow.Models
{
    public partial class Designation
    {

        [Key]
        public int DesignationId { get; set; }

        [StringLength(50)]
        public string DesignationName { get; set; } = null!;
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Designations")]
        public virtual Department Department { get; set; } = null!;

          [InverseProperty("Designation")]
        public virtual ICollection<User> Users { get; set; }= null!;
    }
}
