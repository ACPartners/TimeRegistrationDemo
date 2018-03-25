namespace TimeRegistrationDemo.Data.Entities
{
    public class UserUserRoleEntity
    {
        public UserEntity User { get; set; }
        public UserRoleEntity UserRole { get; set; }

        public long UserId { get; set; }
        public string UserRoleId { get; set; }
    }
}
