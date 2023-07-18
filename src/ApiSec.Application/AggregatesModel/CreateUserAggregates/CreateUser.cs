using ApiSec.Application.AggregatesModel.CreateUserRoleAggregates;
using ApiSec.Application.Repositories;
using ApiSec.Application.Services;
using ApiSec.Core.Entities;

namespace ApiSec.Application.AggregatesModel.CreateUserAggregates;

public class CreateUser : ICreateUser
{
    private readonly IUserRepository userRepository;
    private readonly IAuthService authService;
    private readonly ICreateUserRole createUserRole;

    public CreateUser(IUserRepository userRepository, IAuthService authService, ICreateUserRole createUserRole)
    {
        this.userRepository = userRepository;
        this.authService = authService;
        this.createUserRole = createUserRole;
    }

    public async Task CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = authService.ComputeSha256Hash(request.Password);
        var user = new User(request.Email, passwordHash);

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
        await CreateUserRole(user.Id, cancellationToken);
    }

    private async Task CreateUserRole(Guid userId, CancellationToken cancellationToken)
    {
        var userRole = new CreateUserRoleRequest("user", userId);
        await createUserRole.CreateAsync(userRole, cancellationToken);
    }
}