namespace ApiSec.Application.AggregatesModel.CreateUserRoleAggregates;

public record CreateUserRoleRequest(string Role, Guid UserId);
