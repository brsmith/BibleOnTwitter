using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Automapping;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Configuration
{
    public static class NHibernateConfiguration
    {
        public static NHibernate.Cfg.Configuration Create()
        {
            return Fluently.Configure()
                .Database(GetPersistenceConfig)
                .Mappings(MapEntities)
                .BuildConfiguration();
        }

        private static IPersistenceConfigurer GetPersistenceConfig()
        {
            return MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey("BibleOnTwitter"))
                .UseReflectionOptimizer()
                .AdoNetBatchSize(20);
        }

        private static void MapEntities(MappingConfiguration Mapping)
        {
            Mapping.AutoMappings.Add(AutoMap.AssemblyOf<Tweet>(new AutomappingConfiguration())
                .Conventions.Add(
                    PrimaryKey.Name.Is(i => i.EntityType.Name + "Id"))
                );
        }
    }
}
