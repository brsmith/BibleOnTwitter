using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToTwitter;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly TwitterContext _twitterContext;

        public TwitterService(TwitterContext twitterContext)
        {
            _twitterContext = twitterContext;
        }

        public IEnumerable<AtomEntry> GetBibleTweets(DateTimeOffset? LastTweet)
        {
            var WeekAgo = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(7));
            if (!LastTweet.HasValue || WeekAgo > LastTweet)
                LastTweet = WeekAgo;

            return
                from search in _twitterContext.Search
                where search.Hashtag == "bible" &&
                      search.Type == SearchType.Search &&
                      search.ResultType == ResultType.Recent &&
                      search.Since > LastTweet
                from entry in search.Entries
                select entry;
        }
    }
}
