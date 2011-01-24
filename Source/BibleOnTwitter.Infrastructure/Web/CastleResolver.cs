using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;

namespace BibleOnTwitter.Infrastructure.Web
{
    public class CastleResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _Container;

        public CastleResolver(IWindsorContainer Container)
        {
            _Container = Container;
        }

        public object GetService(Type serviceType)
        {
            if (_Container.Kernel.HasComponent(serviceType))
                return _Container.Resolve(serviceType);
            else
                return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if(_Container.Kernel.HasComponent(serviceType))
                return _Container.ResolveAll(serviceType).Cast<object>();
            else
                return new object[] { };
        }
    }
}
