using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Configuration.DataMapping
{
    public class TweetMapping : ClassMap<Tweet>
    {
        public TweetMapping()
        {
            Table("Tweets");

            Id(x => x.TweetId).GeneratedBy.GuidComb();
            Map(x => x.Id).Not.Nullable().Unique();
            Map(x => x.Content).Not.Nullable();
            Map(x => x.PublishedTime).Not.Nullable();

            HasMany(x => x.BibleVerseReferences).Cascade.All();
            HasManyToMany(x => x.References).Table("Tweets_References").Cascade.All();
            References(x => x.Author).Cascade.All();
        }
    }
}
