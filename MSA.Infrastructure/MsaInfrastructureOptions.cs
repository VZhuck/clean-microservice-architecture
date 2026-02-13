using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MSA.Application.Interfaces;

namespace MSA.Infrastructure;

// TODO: CONSIDER  TO DELETE IF NOT USED 
public class MsaInfrastructureOptions
//     <TDbContext, TDbInitializer>
// where TDbContext: DbContext, IMicroserviceDbContext
// where TDbInitializer: IDbContextInitializer
{
    public required Type DbContext { get; init; }
    
    public required string ConnStrKey { get; init; } 

    public required Type DbContextInitializer { get; init; } 
    //IDbContextInitializer
}

// public interface IMsaInfrastructureOptionsBuilder
// {
//     MsaInfrastructureOptionsBuilder WithDbContextType<TDbContext> ()
//         where TDbContext: DbContext, IMicroserviceDbContext;
//
//     MsaInfrastructureOptionsBuilder WithDbInitializerType<TDbInitializer> ()
//         where TDbInitializer: IDbContextInitializer;
// }
//
// public  class MsaInfrastructureOptionsBuilder : IMsaInfrastructureOptionsBuilder
// {
//     private Type? _dbContextType;
//     private Type? _dbContextInitializer;
//     private string? _connStrKey;
//
//     public MsaInfrastructureOptionsBuilder WithDbContextType<TDbContext> ()
//         where TDbContext: DbContext, IMicroserviceDbContext
//     {
//         _dbContextType = typeof(TDbContext);
//         return this;
//     }
//     
//     public MsaInfrastructureOptionsBuilder WithDbInitializerType<TDbInitializer> ()
//         where TDbInitializer: IDbContextInitializer
//     {
//         _dbContextInitializer = typeof(TDbInitializer);
//         return this;
//     }
//
//     public MsaInfrastructureOptionsBuilder WithConnStrKey(string connStrKey)
//     {
//         _connStrKey  = connStrKey;
//         return this;
//     }
//
//     public MsaInfrastructureOptions Buid()
//     {
//         Guard.Against.Null(_dbContextType);
//         Guard.Against.Null(_dbContextInitializer);
//         Guard.Against.NullOrWhiteSpace(_connStrKey);
//         
//         return new MsaInfrastructureOptions
//         {
//             DbContext = _dbContextType,
//             DbContextInitializer = _dbContextInitializer,
//             ConnStrKey = _connStrKey
//         };
//     }
// }

