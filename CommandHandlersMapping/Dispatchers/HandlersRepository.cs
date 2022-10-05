using Autofac;
using CommandHandlersMapping.Handlers;
using CommandHandlersMapping.Tests;
using System;
using System.Collections.Generic;

namespace CommandHandlersMapping.Dispatchers
{
    public class HandlersRepository : IHandlersRepository
    {
        private static readonly Dictionary<Type, Type> Handlers = new Dictionary<Type, Type>();
        private static readonly object Locker = new object();

        private readonly IComponentContext _resolver;

        public HandlersRepository(IComponentContext resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public object FindByHandlerType(Type handler)
        {
            _resolver.TryResolve(handler, out var result);

            var resfdf = _resolver.Resolve<ITest>();
            var tr = _resolver.TryResolve<ITest>(out var ffs);

            resfdf.WriteToOutput("resolved");
            ffs.WriteToOutput("resolved AGAIN");

            return result;
        }

        public object FindByCommandType(Type commandType, Type resultType = null)
        {
            var handlerType = GetHandlerType(commandType);
            var res = FindByHandlerType(handlerType);
            return res;
        }

        private static Type GetHandlerType(Type type)
        {
            var res = GetHandlerType(type, () => typeof(ICommandHandler<>).MakeGenericType(type));
            return res;
        }

        private static Type GetHandlerType(Type type, Func<Type> predicate)
        {
            lock (Locker)
            {
                if (Handlers.ContainsKey(type))
                {
                    return Handlers[type];
                }

                var handlerType = predicate();
                Handlers[type] = handlerType;

                return handlerType;
            }
        }
    }
}
