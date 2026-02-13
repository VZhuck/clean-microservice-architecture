using Microsoft.EntityFrameworkCore;
using MSA.Application.Interfaces;

namespace Application.Interfaces;

public interface IAppDbContext : IMicroserviceDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
}