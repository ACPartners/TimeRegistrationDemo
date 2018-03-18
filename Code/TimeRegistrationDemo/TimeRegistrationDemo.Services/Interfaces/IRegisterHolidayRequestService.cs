using TimeRegistrationDemo.Services.Dtos;

namespace TimeRegistrationDemo.Services.Interfaces
{
    public interface IRegisterHolidayRequestService
    {
        RegisterHolidayRequestOutputDto Register(RegisterHolidayRequestInputDto request);
    }
}
