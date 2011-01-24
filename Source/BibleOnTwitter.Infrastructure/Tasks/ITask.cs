using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure.Tasks
{
    public interface ITask
    {
        void Execute();
        TimeSpan NextExecutionTime();
    }
}
