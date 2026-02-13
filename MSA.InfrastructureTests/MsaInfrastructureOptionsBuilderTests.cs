using Microsoft.EntityFrameworkCore;
using MSA.Application.Interfaces;
using MSA.Infrastructure;

namespace MSA.InfrastructureTests;

public class MsaInfrastructureOptionsBuilderTests
{
    #region MockClasses

    class TestDbContext : DbContext, IMicroserviceDbContext
    {
        // ReSharper disable once OptionalParameterHierarchyMismatch
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken) => Task.FromResult(0);
    }

    class TestDbContextInitializer : IDbContextInitializer
    {
        public Task InitialiseAsync() => Task.CompletedTask;

        public Task SeedAsync() => Task.CompletedTask;
    }
    #endregion
    
    // [Fact]
    // public void MsaInfrastructureOptionsBuilder_TypesAssigned()
    // {
    //     // Arrange
    //     var builder = new MsaInfrastructureOptionsBuilder();
    //
    //     // Act
    //     var options = builder
    //         .WithDbContextType<TestDbContext>()
    //         .WithDbInitializerType<TestDbContextInitializer>()
    //         .Buid();
    //
    //     // Assert
    //     Assert.Equal(typeof(TestDbContext), options.DbContext);
    //     Assert.Equal(typeof(TestDbContextInitializer), options.DbContextInitializer);
    // }
}