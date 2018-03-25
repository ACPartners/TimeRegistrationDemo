
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingEFCoreInMemory.Models
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        //public ICollection<UserUserRoleEntity> UserRoles { get; set; }
    }
}