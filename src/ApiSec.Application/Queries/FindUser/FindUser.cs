using ApiSec.Application.Repositories;

namespace ApiSec.Application.Queries.FindUser;

public class FindUser : IFindUser
{
    private readonly IUserRepository userRepository;

    public FindUser(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<FindUserResponse?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(id, cancellationToken);

        if(user is null)
            return null;

        var result = new FindUserResponse(user.Id, user.Email, user.Active, user.UserRoles.Select(_ => _.Role));
        return result;
    }

    public async Task<FindUserResponse?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(email, cancellationToken);

        if(user is null)
            return null;

        var result = new FindUserResponse(user.Id, user.Email, user.Active, user.UserRoles.Select(_ => _.Role));
        return result;
    }
}
