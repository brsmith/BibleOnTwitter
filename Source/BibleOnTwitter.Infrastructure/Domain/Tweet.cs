using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter;

namespace BibleOnTwitter.Infrastructure.Domain
{
    public class Tweet
    {
        public virtual Guid TweetId { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string Author { get; set; }
        public virtual string AuthorUrl { get; set; }
        public virtual string ProfileImageUrl { get; set; }

        public virtual IEnumerable<string> GetTags()
        {
            return Title.Words()
                .StartingWith("#");                
        }

        public virtual IEnumerable<string> GetPeople()
        {
            return Title.Words()
                .StartingWith("@");
        }

        public virtual IEnumerable<string> GetTagsAndPeople()
        {
            return this.GetTags().Union(this.GetPeople());
        }
    }
}
