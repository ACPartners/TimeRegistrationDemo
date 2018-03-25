using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeRegistrationDemo.Data.Entities
{
    public class UserEntity
    {
        [Key]
        public long Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        public ICollection<UserUserRoleEntity> UserRoles { get; set; }

        public ICollection<HolidayRequestEntity> HolidayRequests { get; set; }
    }
}