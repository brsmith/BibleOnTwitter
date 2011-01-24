using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter.Infrastructure.Model.View;
using BibleOnTwitter.Infrastructure.DataAccess;
using NHibernate.Linq;
using BibleOnTwitter.Infrastructure.Model.Data;

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
            var IndexView = new IndexView();

            _SessionProvider.Transactional(session =>
                {
                    var Tweets = session.Query<Tweet>()
                        .OrderByDescending(t => t.CreatedAt)
                        .Take(50)
                        .ToList()
                        .Select(t => new BibleTweet
                            {
                                AuthorName = t.Author,
                                AuthorLink = t.AuthorUrl,
                                AuthorImageUrl = t.ProfileImageUrl,
                                Content = t.Title
                            });
                        //.ToFuture();

                    IndexView.BibleTweets = Tweets;
                });

            return IndexView;
        }
    }
}
