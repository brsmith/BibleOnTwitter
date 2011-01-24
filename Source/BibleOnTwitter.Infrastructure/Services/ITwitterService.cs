using System;
using System.Collections.Generic;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Services
{
    public interface ITwitterService
    {
        IEnumerable<Tweet> GetBibleTweets(DateTimeOffset? LastTweet);
    }
}
