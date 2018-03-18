using TimeRegistrationDemo.Data.Entities;

namespace TimeRegistrationDemo.Repositories.Interfaces
{
    public interface IHolidayTypeRepository
    {
        HolidayTypeEntity GetByKey(string key);
    }
}
