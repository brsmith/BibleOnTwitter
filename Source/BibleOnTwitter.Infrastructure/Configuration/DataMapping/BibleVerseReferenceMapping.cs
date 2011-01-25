using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Configuration.DataMapping
{
    public class BibleVerseReferenceMapping : ClassMap<BibleVerseReference>
    {
        public BibleVerseReferenceMapping()
        {
            Table("BibleVerseReferences");

            Id(x => x.BibleVerseReferenceId);
            Map(x => x.Book).Not.Nullable();
            Map(x => x.Chapter).Not.Nullable();
            Map(x => x.VerseStart).Not.Nullable();
            Map(x => x.VerseEnd).Not.Nullable();

            References(x => x.Tweet);
        }
    }
}
