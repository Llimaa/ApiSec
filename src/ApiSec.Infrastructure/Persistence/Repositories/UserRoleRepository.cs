
using ApiSec.Application.Repositories;
using ApiSec.Core.Entities;
using ApiSec.Infrastructure.Persistence;

namespace ApiSec.Infrastructure.Persistence.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApiSecDbContext _dbContext;
        public UserRoleRepository(ApiSecDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserRole userRole)
        {
            await _dbContext.UserRoles.AddAsync(userRole);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
