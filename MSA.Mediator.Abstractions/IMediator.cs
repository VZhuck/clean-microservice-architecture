
namespace MSA.Mediator.Abstractions;

public interface IMediator
{
    // Publisher
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
}