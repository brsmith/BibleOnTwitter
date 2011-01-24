using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter;

namespace BibleOnTwitter.Infrastructure.Model.Data
{
    public class Tweet
    {
        public Tweet()
        {
            References = new HashSet<Reference>();
            BibleVerseReferences = new HashSet<BibleVerseReference>();
        }

        public virtual Guid TweetId { get; set; }

        public virtual string Id { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTimeOffset? PublishedTime { get; set; }

        public virtual Author Author { get; set; }
        public virtual ISet<Reference> References { get; set; }
        public virtual ISet<BibleVerseReference> BibleVerseReferences { get; set; }        
    }
}
