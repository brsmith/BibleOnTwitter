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
                var TotalTweetCount = Session.QueryOver<Tweet>()                   
                    

                Result.TopTags = Session.QueryOver<Reference>()
                    .Fetch(r => r.Tweets).Eager
                    .Select(
                    


                Result.Tweets = Session.QueryOver<Tweet>()
                    .Fetch(t => t.Author).Eager
                    .Fetch(t => t.BibleVerseReferences).Eager
                    .Fetch(t => t.References).Eager
                    .TransformUsing(Transformers.DistinctRootEntity)
                    .List();
            });
            return Result;
        }
    }
}
