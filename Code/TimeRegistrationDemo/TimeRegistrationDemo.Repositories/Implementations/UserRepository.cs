using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TimeRegistrationDemo.Data;
using TimeRegistrationDemo.Data.Entities;
using TimeRegistrationDemo.Repositories.Interfaces;

namespace TimeRegistrationDemo.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly TimeRegistrationDbContext TimeRegistrationDbContext;

        public UserRepository(TimeRegistrationDbContext timeRegistrationDbContext)
        {
            TimeRegistrationDbContext = timeRegistrationDbContext;
        }

        public UserEntity GetByKey(long key)
        {
            return TimeRegistrationDbContext.Users.Single(x => x.Id == key);
        }

        public IList<UserEntity> GetAll()
        {
            return TimeRegistrationDbContext.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.UserRole)
                .ToList();
        }
    }
}
