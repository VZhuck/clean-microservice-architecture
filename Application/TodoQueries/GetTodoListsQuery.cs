using Application.Common;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace Application.TodoQueries;

//[Authorize]
public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetTodosQuery, TodosVm>
{
    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
                .ToList(),

            Lists = await context.TodoLists
                .AsNoTracking()
                .ProjectToType<TodoListDto>(mapper.Config)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}