using Autofac;
using CommandHandlersMapping.CommandHandler;
using CommandHandlersMapping.Commands;
using CommandHandlersMapping.Dispatchers;
using CommandHandlersMapping.Handlers;
using CommandHandlersMapping.Tests;
using System.Reflection;
using Module = Autofac.Module;

namespace CommandHandlersMapping.DependencyModules
{
    public class ModuleBase : Module
    {
        protected Assembly CurrentAssembly => GetType().Assembly;


        // todo wtf is happening here??
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyTest>().As<ITest>();

            //builder.Register(c => new MyTest()).As<ITest>();

            builder.RegisterType<TestCommandHandler>().As<ICommandHandler>().InstancePerLifetimeScope();
            builder.RegisterType<TestCommand>().As<ICommand>().InstancePerLifetimeScope();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<HandlersRepository>().As<IHandlersRepository>().InstancePerLifetimeScope();

            base.Load(builder);

            // RegisterAllHandlersTypes(builder);
        }
    }
}
