
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AspireOverflow.Models
{

    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }


        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }

        [InverseProperty("UserRole")]
        public virtual ICollection<User> Users { get; set; }
    }
}
