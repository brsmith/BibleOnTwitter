using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using LinqToTwitter;
using Castle.Core;

namespace BibleOnTwitter.Infrastructure.Configuration
{
    public class LinqToTwitterInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddComponentLifeStyle<TwitterContext>(LifestyleType.Singleton);
        }
    }
}
