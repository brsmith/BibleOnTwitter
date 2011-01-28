using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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

        public IEnumerable<AtomEntry> GetBibleTweets(DateTimeOffset? LastTweet, IEnumerable<string> AlsoHashTags)
        {
            var WeekAgo = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(7));
            if (!LastTweet.HasValue || WeekAgo > LastTweet)
                LastTweet = WeekAgo;

            return new string[] { "bible" }.Union(AlsoHashTags)
                .SelectMany(tag => GetTweetsForTag(LastTweet, tag))
                .Distinct((a, b) => a.ID == b.ID, a => a.ID.GetHashCode());
        }

        private IEnumerable<AtomEntry> GetTweetsForTag(DateTimeOffset? LastTweet, string Tag)
        {
            try
            {
                return
                    from search in _twitterContext.Search
                    where search.Hashtag == "bible" &&
                          search.Type == SearchType.Search &&
                          search.PageSize == 200 //&&
                    //search.Since >= LastTweet.Value.UtcDateTime 
                    from entry in search.Entries
                    select entry;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<AtomEntry>();
            }
        }
    }
}
