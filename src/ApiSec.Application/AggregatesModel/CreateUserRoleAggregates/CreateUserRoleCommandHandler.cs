using ApiSec.Application.Repositories;
using ApiSec.Core.Entities;

namespace ApiSec.Application.AggregatesModel.CreateUserRoleAggregates;

public class CreateUserRole : ICreateUserRole
{
    private readonly IUserRoleRepository roleRepository;

    public CreateUserRole(IUserRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public async Task CreateAsync(CreateUserRoleRequest request, CancellationToken cancellationToken)
    {
        var userRole = new UserRole(request.UserId, request.Role);
        await roleRepository.AddAsync(userRole);
    }
}