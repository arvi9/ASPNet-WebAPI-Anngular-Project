
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AspireOverflow.Models
{
       public partial class Department
    {
        public Department()
        {
            Designations = new HashSet<Designation>();
           
        }
        public int DepartmentId { get; set; }
     
        [StringLength(50)]
        public string DepartmentName { get; set; } = null!;
        

        [InverseProperty("Department")]
        public virtual ICollection<Designation> Designations { get; set; }
      
    }
}
