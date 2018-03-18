using TimeRegistrationDemo.Data.Entities;

namespace TimeRegistrationDemo.Repositories.Interfaces
{
    public interface IUserRepository
    {
        UserEntity GetByKey(long key);
    }
}
