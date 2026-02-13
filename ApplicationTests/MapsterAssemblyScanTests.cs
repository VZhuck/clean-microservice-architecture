using System.Reflection;
using Application.TodoQueries;
using Mapster;

namespace ApplicationTests;

public class MapsterAssemblyScanTests
{
    // Assembly which keeps all mapping configs
    private readonly Assembly _assembly = typeof(TodoItemDto).Assembly;
    
    [Fact]
    public void MapsterScan_HasAtLeastOneConfigs_Ok()
    {
        // Arrange
        var configs = _assembly
            .GetTypes()
            .Where(type => typeof(IRegister).IsAssignableFrom(type) &&
                           type.IsClass &&
                           !type.IsAbstract);
        
        // Act
        var sut = TypeAdapterConfig.GlobalSettings.Scan(_assembly);
        
        // Assert
        Assert.True(sut.Count > 0,
            $"{_assembly.FullName} has no registered configs but expected to have");
    }
    
    [Fact]
    public void MapsterScan_FindsAllIRegisterConfigs_Ok()
    {
        // Arrange
        var cfgClassNames = _assembly
            .GetTypes()
            .Where(type => typeof(IRegister).IsAssignableFrom(type) &&
                           type.IsClass &&
                           !type.IsAbstract)
            .Select(cfg=>cfg.FullName)
            .ToList();
        
        // Act
        var sut = TypeAdapterConfig.GlobalSettings.Scan(_assembly);
        
        // Assert
        Assert.True(sut.All(r => cfgClassNames.Contains(r.GetType().FullName)),
            "All registered configs (marked with IRegister) should have been found with Mapster's Scan");
    }
}