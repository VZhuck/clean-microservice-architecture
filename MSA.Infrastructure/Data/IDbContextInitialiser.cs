using Microsoft.Extensions.Logging;

namespace MSA.Infrastructure.Data;

public interface IDbContextInitialiser
{
    Task InitialiseAsync();

    Task SeedAsync();
}
