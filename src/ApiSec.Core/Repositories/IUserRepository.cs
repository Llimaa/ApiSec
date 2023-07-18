using ApiSec.Core.Entities;

namespace ApiSec.Application.Repositories;

public interface IUserRepository
{
    Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken); 
    Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken); 
    Task<User> FindByEmailAndPasswordAsync(string email, string passwordHasg, CancellationToken cancellationToken);

    Task AddAsync(User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
