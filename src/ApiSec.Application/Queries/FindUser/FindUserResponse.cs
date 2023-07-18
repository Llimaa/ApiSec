using ApiSec.Core.Entities;

namespace ApiSec.Application.Queries.FindUser;

public record FindUserResponse(Guid Id, string Email, bool Active, IEnumerable<string> Roles);
