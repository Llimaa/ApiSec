using ApiSec.Infrastructure.Persistence.Context;

namespace ApiSec.Api.Configuration;
public class MSSQLConfiguration : IDBConfiguration
{
    private readonly IConfiguration configuration;

    public MSSQLConfiguration(IConfiguration config)
    {
        configuration = config;
    }
    public string ConnectionString => configuration.GetConnectionString("DevFreelaCs") ?? "";
}