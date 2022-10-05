using CommandHandlersMapping.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlersMapping.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task ExecuteAsync<TCommand>(TCommand command)
            where TCommand : ICommand;

        Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand;
    }
}
