using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Facilities.TypedFactory;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using BibleOnTwitter.Infrastructure.Model;
using NHibernate;
using BibleOnTwitter.Infrastructure.DataAccess;
using FluentNHibernate.Conventions.Helpers;
using System.Web;
using Castle.Core;

namespace BibleOnTwitter.Infrastructure.Configuration
{
    public class FluentNHibernateInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISessionFactory>()
                    .LifeStyle.Is(LifestyleType.Singleton)
                    .UsingFactoryMethod(() => NHibernateConfiguration.Create().BuildSessionFactory()),         
                Component.For<INHibernateSessionProvider>()
                    .LifeStyle.Transient
                    .ImplementedBy<ThreadStoreNHibernateSessionProvider>(),
                Component.For<INHibernateSessionProvider>()
                    .LifeStyle.PerWebRequest
                    .ImplementedBy<HttpNHibernateSessionProvider>(),
                Component.For<INHibernateSessionProvider>()
                    .UsingFactoryMethod<INHibernateSessionProvider>((k, ctx) =>
                        {
                            if (HttpContext.Current == null)
                                return k.Resolve<INHibernateSessionProvider>(typeof(ThreadStoreNHibernateSessionProvider).Name);
                            else
                                return k.Resolve<INHibernateSessionProvider>(typeof(HttpNHibernateSessionProvider).Name);
                        }));                
        }
    }
}
