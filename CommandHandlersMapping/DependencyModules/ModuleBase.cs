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
    /// <summary>
    ///  Autofac Modules
    ///  https://autofac.readthedocs.io/en/latest/configuration/modules.html
    /// </summary>
    public class ModuleBase : Module
    {
        protected Assembly CurrentAssembly => GetType().Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyTest>().As<ITest>();

            var assembly = CurrentAssembly;
            builder.RegisterAssemblyTypes(assembly).Where(t => typeof(ICommandHandler).IsAssignableFrom(t)).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<HandlersRepository>().As<IHandlersRepository>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
