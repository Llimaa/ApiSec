namespace ApiSec.Core.Entities;

public class UserRole
{
    public UserRole(Guid idUser, string role)
    {
        Id = Guid.NewGuid();
        IdUser = idUser;
        Role = role;
    }

    public Guid Id { get; private set; }
    public Guid IdUser { get; private set; }
    public string Role { get; private set; } = null!;
    public User User { get; private set; } = null!;
}