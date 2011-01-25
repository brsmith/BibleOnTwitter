using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Configuration.DataMapping
{
    public class ReferenceMapping : ClassMap<Reference>
    {
        public ReferenceMapping()
        {
            Table("[References]");

            Id(x => x.ReferenceId);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Type).CustomType<GenericEnumMapper<ReferenceType>>().Not.Nullable();

            HasManyToMany(x => x.Tweets).Table("Tweets_References");
        }
    }
}
