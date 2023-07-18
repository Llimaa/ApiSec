
using ApiSec.Application.Repositories;
using ApiSec.Application.Services;

namespace ApiSec.Application.AggregatesModel.CreateUserRoleAggregates;

public class LoginUser : ILoginUser
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public LoginUser(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<LoginUserResponse?> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Password);

        var user = await _userRepository.FindByEmailAndPasswordAsync(request.Email, passwordHash, cancellationToken);

        if (user is null) return null;

        var token = _authService.GenerateJwtToken(user.Email, user.UserRoles);
        var loginResponse = new LoginUserResponse(token.Value, token.ExpireAt);
        return loginResponse;
    }
}
