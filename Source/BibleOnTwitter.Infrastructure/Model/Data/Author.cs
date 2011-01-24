using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Model.Data
{
    public class Author
    {
        public static string GetProfileNameFromUrl(string ProfileUrl)
        {
            var LastSlash = ProfileUrl.LastIndexOf("/");
            if (LastSlash == -1)
                throw new ArgumentException("Not a valid twitter profile url", "ProfileUrl");

            return ProfileUrl.Substring(LastSlash);
        }

        public virtual Guid TweetId { get; set; }
        public virtual string Name { get; set; }
        public virtual string ProfileName { get; set; }
        public virtual string ProfileUrl { get; set; }
        public virtual string ImageUrl { get; set; }        
    }
}
