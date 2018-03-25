using TimeRegistrationDemo.Services.Dtos.ListHolidayRequest;

namespace TimeRegistrationDemo.Services.Interfaces
{
    public interface IListHolidayRequestService
    {
        ListHolidayRequestOutputDto List(ListHolidayRequestInputDto request);
    }
}
