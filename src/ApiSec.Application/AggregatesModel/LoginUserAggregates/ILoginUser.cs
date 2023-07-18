
namespace ApiSec.Application.AggregatesModel.CreateUserRoleAggregates;

public interface ILoginUser
{
    Task<LoginUserResponse?> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken);
}
