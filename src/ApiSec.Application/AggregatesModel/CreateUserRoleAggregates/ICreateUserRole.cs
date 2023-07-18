namespace ApiSec.Application.AggregatesModel.CreateUserRoleAggregates;

public interface ICreateUserRole
{
    public Task CreateAsync(CreateUserRoleRequest request, CancellationToken cancellationToken);
}
