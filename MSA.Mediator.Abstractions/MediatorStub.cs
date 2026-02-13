using System.Diagnostics;

namespace MSA.Mediator.Abstractions;

public class MediatorStub : IMediator
{
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) 
        where TNotification : INotification
    {
        Task.Run(() => { Debug.WriteLine($"{notification.GetType().Name} Published"); }, cancellationToken);
        return Task.CompletedTask;
    }
}