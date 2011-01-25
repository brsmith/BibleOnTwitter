using System;
using System.Collections.Generic;
using BibleOnTwitter.Infrastructure.Model.Data;
using LinqToTwitter;

namespace BibleOnTwitter.Infrastructure.Services
{
    public interface ITwitterService
    {
        IEnumerable<AtomEntry> GetBibleTweets(DateTimeOffset? LastTweet);
    }
}
