using CommandHandlersMapping.Commands;
using CommandHandlersMapping.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlersMapping.CommandHandler
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public Task ExecuteAsync(TestCommand command, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
