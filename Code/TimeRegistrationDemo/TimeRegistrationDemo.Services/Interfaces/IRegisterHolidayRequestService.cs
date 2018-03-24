using TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest;

namespace TimeRegistrationDemo.Services.Interfaces
{
    public interface IRegisterHolidayRequestService
    {
        RegisterHolidayRequestOutputDto Register(RegisterHolidayRequestInputDto request);
    }
}
