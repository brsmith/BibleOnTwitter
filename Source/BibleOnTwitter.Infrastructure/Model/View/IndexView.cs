using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Model.View
{
    public class IndexView
    {
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<HashTag> HashTags { get; set; }
        public IEnumerable<BibleTweet> BibleTweets { get; set; }
    }
}
