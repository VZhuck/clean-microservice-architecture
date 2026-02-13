using Mapster;

namespace Application.TodoQueries;

public class TodoItemDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    public int Priority { get; init; }

    public string? Note { get; init; }

    private class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TodoItem, TodoItemDto>()
                .Map(dest => dest.Priority, src => (int)src.Priority);
        }
    }
}