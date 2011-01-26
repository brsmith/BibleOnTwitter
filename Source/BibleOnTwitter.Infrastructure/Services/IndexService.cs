using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter.Infrastructure.Model.View;
using BibleOnTwitter.Infrastructure.DataAccess;
using NHibernate.Linq;
using NHibernate.Transform;
using BibleOnTwitter.Infrastructure.Model.Data;
using NHibernate.Criterion;

namespace BibleOnTwitter.Infrastructure.Services
{
    public class IndexService : IIndexService
    {
        private readonly INHibernateSessionProvider _SessionProvider;

        public IndexService(INHibernateSessionProvider SessionProvider)
        {
            _SessionProvider = SessionProvider;
        }

        public IndexView GetIndexView()
        {
            var Result = new IndexView { Tweets = Enumerable.Empty<Tweet>() };
            _SessionProvider.Transactional(Session =>
            {
                var ATopTags = Session.Query<Reference>()
                    .Where(r => r.Type == ReferenceType.HashTag)
                    .Select(r => new { HashTag = r.Name, TweetCount = r.Tweets.Count });

                Result.Tweets = Session.QueryOver<Tweet>()
                    .Fetch(t => t.Author).Eager
                    .Fetch(t => t.BibleVerseReferences).Eager
                    .Fetch(t => t.References).Eager
                    .TransformUsing(Transformers.DistinctRootEntity)
                    .List();

                var TotalTweetCount = Session.QueryOver<Tweet>()
                     .RowCount();

                Result.TopTags = ATopTags
                    .Select(t => new TagReferenceCounter
                    {
                        HashTag = t.HashTag,
                        TweetCount = t.TweetCount,
                        TweetTotalCount = TotalTweetCount
                    }).ToArray();
            });
            return Result;
        }
    }
}
