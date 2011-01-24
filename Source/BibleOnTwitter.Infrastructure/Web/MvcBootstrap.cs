using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;

namespace BibleOnTwitter.Infrastructure.Web
{
    public static class MvcBootstrap
    {
        public static WindsorContainer Start()
        {
            var Container = new WindsorContainer();            
            
            Container.Install(typeof(MvcBootstrap).Assembly
                .GetTypes().Where(t => t.GetInterface("IWindsorInstaller") != null)
                .Select(t => Activator.CreateInstance(t) as IWindsorInstaller)
                .ToArray());      
     
            return Container;
        }
    }
}
