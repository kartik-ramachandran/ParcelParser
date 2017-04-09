using Autofac;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using Autofac.Core.Activators.Reflection;

namespace ParcelLoader.Core
{
    public class Ioc
    {
        public static IContainer Container { get; set; }

        public static void SetDIContainer()
        {
            var builder = new ContainerBuilder();

            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();

            assemblies.ToList().Add(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(assemblies.ToArray())
            .Where(t => t.GetCustomAttributes(typeof(RegisterSingletonAttribute), false).Any())
            .UsingConstructor(new DefaultConstructorSelector())
            .AsSelf()
            .AsImplementedInterfaces();          

            Container = builder.Build();
        }

        public static T GetInstance<T>()
        {
            using (var httpRequestScope = Container.BeginLifetimeScope())
            {
                if (httpRequestScope.IsRegistered<T>())
                {
                    return httpRequestScope.Resolve<T>();
                }
            }
            return default(T);
        }
    }    

    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterSingletonAttribute : Attribute { }

    public class DefaultConstructorSelector : IConstructorSelector
    {
        public ConstructorParameterBinding SelectConstructorBinding(
            ConstructorParameterBinding[] constructorBindings)
        {
            var defaultConstructor = constructorBindings.SingleOrDefault(c => c.TargetConstructor.GetParameters().Length > 0);
            if (defaultConstructor == null)
            {
                defaultConstructor = constructorBindings.SingleOrDefault(c => c.TargetConstructor.GetParameters().Length == 0);
            }
            return defaultConstructor;
        }
    }
}
