
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspireOverflow.Models
{
    [Table("Gender")]
    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }

        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenderId { get; set; }
      
        [StringLength(15)]
        public string Name { get; set; } = null!;

        [InverseProperty("Gender")]
        public virtual ICollection<User> Users { get; set; }
    }
}
