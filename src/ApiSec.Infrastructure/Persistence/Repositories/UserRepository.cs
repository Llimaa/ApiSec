using ApiSec.Application.Repositories;
using ApiSec.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiSec.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiSecDbContext dbContext;

        public UserRepository(ApiSecDbContext dbContext) =>
            (this.dbContext) = (dbContext);


        public async Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await dbContext.Users
                .Include(u => u.UserRoles)
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

        public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken) =>
            await dbContext
                .Users
                .Include(u => u.UserRoles)
                .SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

        public async Task<User> FindByEmailAndPasswordAsync(string email, string passwordHasg, CancellationToken cancellationToken) =>
            await dbContext
                .Users
                .Include(u => u.UserRoles)
                .SingleOrDefaultAsync(x => x.Email == email && x.Password == passwordHasg, cancellationToken);


        public async Task AddAsync(User user, CancellationToken cancellationToken) =>
            await dbContext.Users.AddAsync(user);

        public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
            await dbContext.SaveChangesAsync(cancellationToken);
    }
}
