using Cortex.Mediator.Commands;

namespace Q2.Web_Service.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

public class LogginCommandBehavior<TCommand> : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    public async Task Handle(TCommand command, CommandHandlerDelegate next, CancellationToken cancellationToken)
    {
        await next();
    }
}