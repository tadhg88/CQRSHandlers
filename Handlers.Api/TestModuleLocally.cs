using Autofac;
using CommandHandlersMapping.CommandHandler;
using CommandHandlersMapping.Commands;
using CommandHandlersMapping.DependencyModules;
using CommandHandlersMapping.Dispatchers;
using CommandHandlersMapping.Handlers;
using CommandHandlersMapping.Tests;

namespace Handlers.Api
{
    public class TestModuleLocally : Module
    {
        // todo wtf is happening here??
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<MyTest>().As<ITest>();

            builder.Register(c => new MyTest()).As<ITest>();

            // builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            //builder.RegisterType<TestCommandHandler>().As<ICommandHandler>().InstancePerLifetimeScope();
            //builder.RegisterType<TestCommand>().As<ICommand>().InstancePerLifetimeScope();
            //builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope(); ;
            //builder.RegisterType<HandlersRepository>().As<IHandlersRepository>().InstancePerLifetimeScope();

            // RegisterAllHandlersTypes(builder);
        }
    }
}
