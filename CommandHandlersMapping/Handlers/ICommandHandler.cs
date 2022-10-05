using CommandHandlersMapping.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlersMapping.Handlers
{
    public interface ICommandHandler
    {
    }

    // todo what is this in below??
    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
