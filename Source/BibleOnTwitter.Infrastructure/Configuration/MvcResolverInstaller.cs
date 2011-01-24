using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using BibleOnTwitter.Infrastructure.Web;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;

namespace BibleOnTwitter.Infrastructure.Configuration
{
   public class MvcResolverInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            DependencyResolver.SetResolver(new CastleResolver(container)); 

            container.Register(
                Component.For<IControllerFactory>().ImplementedBy<DefaultControllerFactory>(),
                Component.For<IControllerActivator>().ImplementedBy<CastleControllerActivator>());
        }
    }
}
