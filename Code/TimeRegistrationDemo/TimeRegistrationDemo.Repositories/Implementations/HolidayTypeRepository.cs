using System.Linq;
using TimeRegistrationDemo.Data;
using TimeRegistrationDemo.Data.Entities;
using TimeRegistrationDemo.Repositories.Interfaces;

namespace TimeRegistrationDemo.Repositories.Implementations
{
    public class HolidayTypeRepository : IHolidayTypeRepository
    {
        private readonly TimeRegistrationDbContext TimeRegistrationDbContext;

        public HolidayTypeRepository(TimeRegistrationDbContext timeRegistrationDbContext)
        {
            TimeRegistrationDbContext = timeRegistrationDbContext;
        }

        public HolidayTypeEntity GetByKey(string key)
        {
            return TimeRegistrationDbContext.HolidayTypes.Single(x => x.Id == key);
        }

        public bool ExistsByKey(string key)
        {
            return TimeRegistrationDbContext.HolidayTypes.Any(x => x.Id == key);
        }
    }
}
