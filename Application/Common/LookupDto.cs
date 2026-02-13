using Application.TodoQueries;
using Mapster;

namespace Application.Common;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }
    
    private class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TodoList, TodoList>();
            config.NewConfig<TodoItem, TodoList>();
        }
    }
}