namespace ApiSec.Application.AggregatesModel.CreateUserAggregates;

public interface ICreateUser
{
    public Task CreateAsync(CreateUserRequest command, CancellationToken cancellationToken);
}
