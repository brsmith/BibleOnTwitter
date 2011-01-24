using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter.Infrastructure.DataAccess;
using BibleOnTwitter.Infrastructure.Model.Data;
using NHibernate.Linq;

namespace BibleOnTwitter.Infrastructure.Services
{
    public class BibleTweetRetrievalService : IBibleTweetRetrievalService
    {
        private readonly ITwitterService _TwitterService;
        private readonly INHibernateSessionProvider _SessionProvider;

        public BibleTweetRetrievalService(ITwitterService TwitterService, INHibernateSessionProvider SessionProvider)
        {
            _TwitterService = TwitterService;
            _SessionProvider = SessionProvider;
        }

        public void GetAndSaveNewTweets()
        {
            _SessionProvider.Transactional(session =>
                {
                    var LastTweetTime =
                        (from tweet in session.Query<Tweet>()
                         orderby tweet.PublishedTime descending
                         select tweet.PublishedTime).FirstOrDefault();

                    var NewTweets =
                        from tweet in _TwitterService.GetBibleTweets(LastTweetTime).AsQueryable<Tweet>()
                        select tweet;

                    foreach (var NewTweet in NewTweets)
                        session.Save(NewTweet);
                });
        }
    }
}
