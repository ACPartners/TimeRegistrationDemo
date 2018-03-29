using System.Collections.Generic;

namespace QuickstartIdentityServer
{
    public class GetAllUsersForAuthenticationOutputDto
    {
        public IList<UserWithRoles> Users { get; set; }
    }

    public class UserWithRoles
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }

    public class RoleDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }
}