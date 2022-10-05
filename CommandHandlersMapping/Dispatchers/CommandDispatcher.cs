using Autofac.Core;
using CommandHandlersMapping.Commands;
using CommandHandlersMapping.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlersMapping.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IHandlersRepository _handlersRepository;

        public CommandDispatcher(IHandlersRepository handlersRepository)
        {
            _handlersRepository = handlersRepository ?? throw new ArgumentNullException(nameof(handlersRepository));
        }

        public async Task ExecuteAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            await ExecuteAsync(command, new CancellationToken());
        }

        public async Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command), "command cannot be null");

            var commandType = command.GetType();
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
            object handler = GetHandler(commandType);

            var methodName = nameof(ICommandHandler<ICommand>.ExecuteAsync);
            var method = GetMethod(methodName, handlerType, commandType);

            var task = (Task)method.Invoke(handler, new object[]
            {
                command,
                cancellationToken
            });
            await task;
        }

        private object GetHandler(Type commandType, Type resultType = null)
        {
            object handler = _handlersRepository.FindByCommandType(commandType, resultType);
            if (handler == null)
            {
                throw new DependencyResolutionException($"Handler for Command {commandType.Name} not registered.");
            }
            return handler;
        }

        protected System.Reflection.MethodInfo GetMethod(string methodName, Type handlerType, Type commandType)
        {
            var method = handlerType.GetMethod(
                methodName,
                new[]
                {
                    commandType,
                    typeof(CancellationToken),
                });

            if (method == null)
            {
                throw new DependencyResolutionException($"Method {methodName} was not found in Handler {handlerType.Name} for Command {commandType.Name}.");
            }

            return method;
        }
    }
}