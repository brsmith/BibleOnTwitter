using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter.Infrastructure.Tasks;

namespace BibleOnTwitter.Infrastructure.Tasks
{
    public interface ITaskFactory
    {
        ITask GetTask(string Name);
        ITask[] GetAllTasks();
        void DestroyTask(ITask Task);
    }
}
