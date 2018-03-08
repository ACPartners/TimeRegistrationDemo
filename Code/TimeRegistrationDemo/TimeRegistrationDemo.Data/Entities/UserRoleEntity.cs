using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeRegistrationDemo.Data.Entities
{
    public class UserRoleEntity
    {
        [Key]
        [StringLength(1, MinimumLength = 1)]
        public string Id { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public ICollection<UserUserRoleEntity> Users { get; set; }
    }
}
