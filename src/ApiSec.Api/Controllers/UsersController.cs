using ApiSec.Application.AggregatesModel.CreateUserAggregates;
using ApiSec.Application.AggregatesModel.CreateUserRoleAggregates;
using ApiSec.Application.Queries.FindUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSec.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ICreateUser createUser;
    private readonly ILoginUser loginUser;
    private readonly IFindUser findUser;

    public UsersController(ICreateUser createUser, ILoginUser loginUser, IFindUser findUser)
    {
        this.createUser = createUser;
        this.loginUser = loginUser;
        this.findUser = findUser;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await createUser.CreateAsync(request, cancellationToken);
            
            // return CreatedAtAction(nameof(GetById), new { id = 1 }, request);
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var loginUserViewModel = await loginUser.LoginAsync(request, cancellationToken);

        if (loginUserViewModel is null) return BadRequest();

        return Ok(loginUserViewModel);
    }

    [HttpGet("user/{id:Guid}")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> Login(Guid id, CancellationToken cancellationToken)
    {
        var response = await findUser.FindByIdAsync(id, cancellationToken);

        if (response is null) return NotFound($"Usuário com {id} não encontrado");

        return Ok(response);
    }

    [HttpGet("user/{email}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Login(string email, CancellationToken cancellationToken)
    {
        var response = await findUser.FindByEmailAsync(email, cancellationToken);

        if (response is null) return NotFound($"Usuário com {email} não encontrado");

        return Ok(response);
    }
}
