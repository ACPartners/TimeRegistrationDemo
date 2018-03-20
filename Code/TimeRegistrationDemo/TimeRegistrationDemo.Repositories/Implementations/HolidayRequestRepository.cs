using TimeRegistrationDemo.Data;
using TimeRegistrationDemo.Data.Entities;
using TimeRegistrationDemo.Repositories.Interfaces;

namespace TimeRegistrationDemo.Repositories.Implementations
{
    public class HolidayRequestRepository : IHolidayRequestRepository
    {
        private readonly TimeRegistrationDbContext TimeRegistrationDbContext;

        public HolidayRequestRepository(TimeRegistrationDbContext timeRegistrationDbContext)
        {
            TimeRegistrationDbContext = timeRegistrationDbContext;
        }

        public void Register(HolidayRequestEntity holidayRequest)
        {
            TimeRegistrationDbContext.HolidayRequests.Add(holidayRequest);
            TimeRegistrationDbContext.SaveChanges();
        }
    }
}
