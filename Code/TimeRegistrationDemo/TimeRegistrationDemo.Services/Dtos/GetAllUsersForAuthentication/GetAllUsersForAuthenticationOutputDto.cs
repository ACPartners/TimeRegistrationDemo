using System.Collections.Generic;

namespace TimeRegistrationDemo.Services.Dtos.ListHolidayRequest
{
    public class GetAllUsersForAuthenticationOutputDto
    {
        public IList<UserWithRoles> Users { get; set; }

        public GetAllUsersForAuthenticationOutputDto(IList<UserWithRoles> users)
        {
            Users = users;
        }
    }
}