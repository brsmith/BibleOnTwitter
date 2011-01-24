using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure
{
    public static class StringExtensions
    {
        public static IEnumerable<string> Words(this string self)
        {
            if (self == null)
                throw new NullReferenceException();

            return self
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s));
        }

        public static IEnumerable<string> StartingWith(this IEnumerable<string> self, string Match)
        {
            if (self == null)
                throw new NullReferenceException();

            return self.Where(s => s.StartsWith(Match));
        }
    }
}
