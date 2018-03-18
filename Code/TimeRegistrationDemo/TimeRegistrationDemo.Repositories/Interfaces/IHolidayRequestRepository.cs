using TimeRegistrationDemo.Data.Entities;

namespace TimeRegistrationDemo.Repositories.Interfaces
{
    public interface IHolidayRequestRepository
    {
        void Register(HolidayRequestEntity holidayRequest); 
    }
}
