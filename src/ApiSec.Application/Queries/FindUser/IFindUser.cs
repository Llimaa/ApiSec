namespace ApiSec.Application.Queries.FindUser;

public interface IFindUser 
{
    Task<FindUserResponse?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<FindUserResponse?> FindByEmailAsync(string email, CancellationToken cancellationToken);
}
