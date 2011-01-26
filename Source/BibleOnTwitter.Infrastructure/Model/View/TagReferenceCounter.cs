using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Model.View
{
    public class TagReferenceCounter
    {
        public string HashTag { get; set; }
        public int TweetCount { get; set; }
        public int TweetTotalCount { get; set; }

        public float Percentage
        {
            get 
            {
                if (TweetCount >= TweetTotalCount)
                    return 100;
                else
                    return ((float)TweetCount / (float)TweetTotalCount) * 100f;
            }
        }
    }
}
