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
        public virtual Guid ReferenceId { get; set; }
        public virtual string Name { get; set; }
    }
}
