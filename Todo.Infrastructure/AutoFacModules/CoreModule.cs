using System.Reflection;
using Autofac;
using Todo.Infrastructure.Helper;
using Module = Autofac.Module;

namespace Todo.Infrastructure.AutoFacModules;

public class CoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        #region Repository
        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces();
        #endregion
        
        #region Services.
        
        var serviceAssembly = AssemblyHelper.GetAssembly("Todo.AppService");
        var registerAssemblyTypes = serviceAssembly == null ? new Assembly[] { } : new[] { serviceAssembly };
        builder.RegisterAssemblyTypes(registerAssemblyTypes)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces();
        
        #endregion
    }
}