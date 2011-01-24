using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Facilities.TypedFactory;
using System.Reflection;

namespace BibleOnTwitter.Infrastructure.Tasks
{
    public class TaskComponentSelector : DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (method.Name == "GetTask" && arguments.Length == 1 && arguments[0] is string)
            {
                return (string)arguments[0];
            }
            
            return base.GetComponentName(method, arguments);
        }        
    }
}
