using ApiSec.Core.Entities;

namespace ApiSec.Application.Services;

public interface IAuthService
{
    Token GenerateJwtToken(string email, List<UserRole> roles);
    string ComputeSha256Hash(string password);
}