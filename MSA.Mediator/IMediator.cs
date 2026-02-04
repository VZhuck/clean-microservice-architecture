
namespace MSA.Mediator;

public interface IMediator
{
    // Publisher
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
}