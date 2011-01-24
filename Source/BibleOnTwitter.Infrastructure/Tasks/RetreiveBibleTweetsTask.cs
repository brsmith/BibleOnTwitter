using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter.Infrastructure.Services;

namespace BibleOnTwitter.Infrastructure.Tasks
{
    public class RetreiveBibleTweetsTask : ITask
    {
        private readonly IBibleTweetRetrievalService _TweetRetrievalService;

        public RetreiveBibleTweetsTask(IBibleTweetRetrievalService TweetRetrievalService)
        {
            _TweetRetrievalService = TweetRetrievalService;
        }

        public void Execute()
        {
            _TweetRetrievalService.GetAndSaveNewTweets();
        }

        public TimeSpan NextExecutionTime()
        {
            return TimeSpan.FromMinutes(1);
        }
    }
}
