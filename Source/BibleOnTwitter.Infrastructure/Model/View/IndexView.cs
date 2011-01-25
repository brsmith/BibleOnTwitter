using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter.Infrastructure.Model.Data;

namespace BibleOnTwitter.Infrastructure.Model.View
{
    public class IndexView
    {
        public IEnumerable<Tweet> Tweets { get; set; }
        public IEnumerable<TagReferenceCounter> TopTags { get; set; }
    }
}
