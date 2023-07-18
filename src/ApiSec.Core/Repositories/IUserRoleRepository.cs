using ApiSec.Core.Entities;

namespace ApiSec.Application.Repositories;

public interface IUserRoleRepository 
{
    Task AddAsync(UserRole userRole);
}