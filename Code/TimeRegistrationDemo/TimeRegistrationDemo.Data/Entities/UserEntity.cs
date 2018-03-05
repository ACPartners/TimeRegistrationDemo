using System.Collections.Generic;

namespace TimeRegistrationDemo.Data.Entities
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<UserRoleEntity> UserRoles { get; set; } //many to many
    }
}