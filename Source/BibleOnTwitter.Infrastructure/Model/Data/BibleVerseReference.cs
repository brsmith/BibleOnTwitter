using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Model.Data
{
    public class BibleVerseReference
    {
        public virtual Guid BibleVerseReferenceId { get; set; }
        public virtual string Book { get; set; }
        public virtual int Chapter { get; set; }
        public virtual int VerseStart { get; set; }
        public virtual int VerseEnd { get; set; }

        public override string ToString()
        {
            if (VerseEnd == VerseStart)
                return string.Format("{0} {1}:{2}", Book, Chapter, VerseStart);
            else
                return string.Format("{0} {1}:{2}-{3}", Book, Chapter, VerseStart, VerseEnd);
        }
    }
}
