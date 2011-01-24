using System;

namespace BibleOnTwitter.Infrastructure.Services
{
    public interface IBibleTweetRetrievalService
    {
        void GetAndSaveNewTweets();
    }
}
