using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Configuration
{
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        private readonly string _ModelNamespace;

        public AutomappingConfiguration()
        {
            _ModelNamespace = typeof(Tweet).Namespace;
        }

        public override bool IsId(Member member)
        {
            if (member.PropertyType == typeof(Guid))
            {
                if (member.Name.EndsWith("Id"))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool ShouldMap(Type type)
        {
            return (type.Namespace == _ModelNamespace);
        }
    }
}
