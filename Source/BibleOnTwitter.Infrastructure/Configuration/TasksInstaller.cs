using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.SubSystems.Configuration;
using BibleOnTwitter.Infrastructure.Tasks;
using Castle.Core;

namespace BibleOnTwitter.Infrastructure.Configuration
{
    public class TasksInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                   .FromThisAssembly()
                   .BasedOn<ITask>()
                   .Configure(c => c
                       .Named(c.ServiceType.Name)
                       .LifeStyle.Is(LifestyleType.Transient))
                   .WithService.FromInterface(typeof(ITask)),
                Component.For<ITaskFactory>()
                    .LifeStyle.Is(LifestyleType.Transient)
                    .AsFactory(c => c.SelectedWith(new TaskComponentSelector()))
           );
        }
    }
}
