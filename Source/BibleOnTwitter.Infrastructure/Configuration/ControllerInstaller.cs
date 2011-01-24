using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Reflection;
using System.Web.Mvc;
using Castle.Core;

namespace BibleOnTwitter.Infrastructure.Configuration
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var Assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("BibleOnTwitter"))
                .Where(a => a.GetTypes().Count(t => t.GetInterface("IController") != null) > 0);

            foreach (var Assembly in Assemblies)
            {
                container.Register(AllTypes.FromAssembly(Assembly)
                    .BasedOn<IController>()                    
                    .WithService.FirstInterface()
                    .Configure(c => c
                        .Named(c.Implementation.Name)
                        .LifeStyle.Is(LifestyleType.Transient)));
            }
        }
    }
}
