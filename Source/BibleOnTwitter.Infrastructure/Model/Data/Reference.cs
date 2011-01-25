using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Model.Data
{
    public enum ReferenceType
    {
        HashTag,
        Person
    }

    public class Reference
    {
        public Reference()
        {
            Tweets = new List<Tweet>();
        }

        public virtual Guid ReferenceId { get; set; }
        public virtual string Name { get; set; }
        public virtual ReferenceType Type { get; set; }

        public virtual IList<Tweet> Tweets { get; set; }
    }
}
