using System.Data;

namespace ApiSec.Infrastructure.Persistence.Context;
public interface IDB : IDisposable
{
    Task<IDbConnection> GetConAsync();
}
