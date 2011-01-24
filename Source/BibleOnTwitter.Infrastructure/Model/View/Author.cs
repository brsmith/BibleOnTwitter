using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Model.View
{
    public class Author
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public int TweetCount { get; set; }
    }
}
