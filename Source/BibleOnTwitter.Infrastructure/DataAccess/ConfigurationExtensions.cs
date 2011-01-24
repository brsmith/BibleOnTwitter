using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;

namespace BibleOnTwitter.Infrastructure.DataAccess
{
    public static class ConfigurationExtensions
    {
        public static void CreateDatabase(this NHibernate.Cfg.Configuration self)
        {
            new SchemaExport(self).Create(true, true);
        }

        public static void DropDatabase(this NHibernate.Cfg.Configuration self)
        {
            new SchemaExport(self).Drop(true, true);
        }

        public static void CreateAndDropDatabase(this NHibernate.Cfg.Configuration self)
        {
            new SchemaExport(self).Execute(true, true, false);
        }
    }
}
