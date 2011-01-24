using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Model.View
{
    public class VerseInfo
    {
        public string Book { get; set; }
        public int Chapter { get; set; }
        public int VerseStart { get; set; }
        public int VerseEnd { get; set; }

        public override string ToString()
        {
            if (VerseEnd == VerseStart)
                return string.Format("{0} {1}:{2}", Book, Chapter, VerseStart);
            else
                return string.Format("{0} {1}:{2}-{3}", Book, Chapter, VerseStart, VerseEnd);
        }
    }
}
