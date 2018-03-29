using System.Collections.Generic;
using TimeRegistrationDemo.Services.Dtos.Shared;

namespace TimeRegistrationDemo.Services.Dtos.ListHolidayRequest
{
    public class UserWithRoles
    {
        public long Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public IList<RoleDto> Roles { get; }

        public UserWithRoles(long id, string firstName, string lastName, IList<RoleDto> roles)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Roles = roles;
        }
    }
}