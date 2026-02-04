namespace MSA.Application.Interfaces;

public interface IMicroserviceDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
