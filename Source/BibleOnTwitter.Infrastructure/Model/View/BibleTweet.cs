using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Model.View
{
    public class BibleTweet
    {        
        public string AuthorName { get; set; }
        public string AuthorLink { get; set; }
        public string AuthorImageUrl { get; set; }

        public string Content { get; set; }

        public IEnumerable<VerseInfo> GetBibleVerses()
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
                    return new VerseInfo
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
