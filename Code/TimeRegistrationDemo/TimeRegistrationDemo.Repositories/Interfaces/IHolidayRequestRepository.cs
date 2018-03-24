using System;
using TimeRegistrationDemo.Data.Entities;

namespace TimeRegistrationDemo.Repositories.Interfaces
{
    public interface IHolidayRequestRepository
    {
        void Register(HolidayRequestEntity holidayRequest);
        bool ExistsByToAndFrom(DateTime from, DateTime to, long userId);
    }
}
