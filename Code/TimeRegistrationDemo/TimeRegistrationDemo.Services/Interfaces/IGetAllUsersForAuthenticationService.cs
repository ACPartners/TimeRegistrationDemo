using TimeRegistrationDemo.Services.Dtos.ListHolidayRequest;

namespace TimeRegistrationDemo.Services.Interfaces
{
    public interface IGetAllUsersForAuthenticationService
    {
        GetAllUsersForAuthenticationOutputDto GetAllUsers();
    }
}
