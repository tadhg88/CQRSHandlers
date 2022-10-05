using Autofac;
using Autofac.Features.ResolveAnything;
using CommandHandlersMapping.CommandHandler;
using CommandHandlersMapping.Commands;
using CommandHandlersMapping.Dispatchers;
using CommandHandlersMapping.Handlers;
using CommandHandlersMapping.Tests;

namespace CommandHandlersMapping.DependencyModules
{
    public class TestModule : ModuleBase
    {
        // todo wtf is happening here??
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<MyTest>().As<ITest>();

            builder.Register(c => new MyTest()).As<ITest>();

            //builder.RegisterType<TestCommandHandler>().As<ICommandHandler>().InstancePerLifetimeScope();
            //builder.RegisterType<TestCommand>().As<ICommand>().InstancePerLifetimeScope();
            //builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            //builder.RegisterType<HandlersRepository>().As<IHandlersRepository>().InstancePerLifetimeScope();

            base.Load(builder);

            // RegisterAllHandlersTypes(builder);
        }
    }
}
