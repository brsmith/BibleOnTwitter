using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using BibleOnTwitter.Infrastructure.Services;
using Castle.Core;

namespace BibleOnTwitter.Infrastructure.Configuration
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes
                .FromThisAssembly()                
                .Where(t => t.Namespace == typeof(ITwitterService).Namespace)
                .WithService.DefaultInterface()
                .Configure(c => c.LifeStyle.Is(LifestyleType.Transient))
            );
        }
    }
}
