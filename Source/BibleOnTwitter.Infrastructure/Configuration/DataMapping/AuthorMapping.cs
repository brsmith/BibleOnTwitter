using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Configuration.DataMapping
{
    public class AuthorMapping : ClassMap<Author>
    {
        public AuthorMapping()
        {
            Table("Authors");

            Id(x => x.AuthorId).GeneratedBy.GuidComb();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.ProfileName).Not.Nullable();
            Map(x => x.ProfileUrl).Not.Nullable();
            Map(x => x.ImageUrl);

            HasMany(x => x.Tweets);
        }
    }
}
