namespace ApiSec.Core.Entities;

public class User
{
    public User(string email, string password)
    {
        Id = Guid.NewGuid();
        Active = true;
        Email = email;
        Password = password;
    }

    public Guid Id { get; private set; }
    public string Email { get; private set; } = null!;
    public bool Active { get; private set; }
    public string Password { get; private set; }  = null!;
    public List<UserRole> UserRoles { get; private set; } = null!;
}
