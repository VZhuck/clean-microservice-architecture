using Application.Interfaces;

namespace Application.TodoLists;

public class CreateTodoListCommandValidator: AbstractValidator<CreateTodoListCommand>
{
    private readonly IAppDbContext _context;

    public CreateTodoListCommandValidator(IAppDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
            .WithMessage("'{PropertyName}' must be unique.")
            .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return !await _context.TodoLists
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}