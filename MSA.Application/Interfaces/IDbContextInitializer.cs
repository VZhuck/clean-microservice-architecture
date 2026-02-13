namespace MSA.Application.Interfaces;

public interface IDbContextInitializer
{
    Task InitialiseAsync();

    Task SeedAsync();
}
