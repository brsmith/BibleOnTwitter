using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;
using System.Web.Routing;
using Castle.MicroKernel;

namespace BibleOnTwitter.Infrastructure.Web
{
    public class CastleControllerActivator : IControllerActivator
    {
        private readonly IKernel _Kernel;

        public CastleControllerActivator(IKernel Kernel)
        {
            _Kernel = Kernel;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            var Key = controllerType.Name;
            var Controller = _Kernel.Resolve<IController>(Key);
            if (Controller != null)
            {
                if (Controller.GetType() == controllerType)
                {
                    return Controller;
                }
            }

            return null;
        }
    }
}
