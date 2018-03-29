using System.Linq;
using TimeRegistrationDemo.Repositories.Interfaces;
using TimeRegistrationDemo.Services.Dtos.ListHolidayRequest;
using TimeRegistrationDemo.Services.Dtos.Shared;
using TimeRegistrationDemo.Services.Interfaces;

namespace TimeRegistrationDemo.Services.Implementations
{
    public class GetAllUsersForAuthenticationService : IGetAllUsersForAuthenticationService
    {
        private readonly IUserRepository UserRepository;

        public GetAllUsersForAuthenticationService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public GetAllUsersForAuthenticationOutputDto GetAllUsers()
        {
            var users = UserRepository.GetAll();

            return new GetAllUsersForAuthenticationOutputDto(
                users.Select(x =>
                    new UserWithRoles(
                        x.Id,
                        x.FirstName,
                        x.LastName,
                        x.UserRoles.Select(y => new RoleDto(y.UserRole.Id, y.UserRole.Description)).ToList())
                ).ToList()
            );
        }
    }
}
