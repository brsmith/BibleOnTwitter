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
            References = new List<Reference>();
            BibleVerseReferences = new List<BibleVerseReference>();
        }

        public virtual Guid TweetId { get; set; }

        public virtual long Id { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTimeOffset? PublishedTime { get; set; }        

        public virtual Author Author { get; set; }
        public virtual IList<Reference> References { get; set; }
        public virtual IList<BibleVerseReference> BibleVerseReferences { get; set; }      

        public virtual IEnumerable<string> ParseReferenceNames()
        {
            return Content.Words()
                .Where(w => w.StartsWith("#") || w.StartsWith("@"))
                .Select(w => w.ToLower().Replace(":", "").Replace(".", "").Replace(",", ""));
        }

        public virtual IEnumerable<BibleVerseReference> ParseBibleVerses()
        {
            var Words = Content.Words();

            return Words
                .Select((s, i) => new { Word = s, Index = i })
                .Where(s => s.Word.Contains(":"))
                .SelectMany(s => s.Word.Split(':')
                    .SelectMany(w => w.Split('-'))
                    .Select(w => new { WordPart = w, WordIndex = s.Index }))
                .Select(wp =>
                {
                    int number = -1;
                    var ok = int.TryParse(wp.WordPart, out number);
                    return new { Number = number, Parsed = ok, WordIndex = wp.WordIndex };
                })
                .GroupBy(w => w.WordIndex)
                .Where(g => g.Count(w => !w.Parsed) == 0)
                .Where(g => g.Count() > 1)
                .Select(g =>
                {
                    var Numbers = g.Select(w => w.Number).ToArray();
                    return new BibleVerseReference
                    {
                        Book = Words.ElementAt(g.Key - 1),
                        Chapter = Numbers[0],
                        VerseStart = Numbers[1],
                        VerseEnd = Numbers.Last()
                    };
                });
        }
    }
}
