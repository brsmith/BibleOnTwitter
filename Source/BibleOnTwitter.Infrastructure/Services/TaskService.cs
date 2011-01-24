using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using BibleOnTwitter.Infrastructure.Tasks;
using System.Reflection;

namespace BibleOnTwitter.Infrastructure.Services
{
    public class TaskService : ITaskService, IDisposable
    {
        private readonly ITaskFactory _TaskFactory;

        public TaskService(ITaskFactory TaskFactory)
        {
            _TaskFactory = TaskFactory;           
        }

        public void Start()
        {
            FirstRun();
        }

        public void Stop()
        {
        }

        public void Dispose()
        {
            this.Stop();
        }

        private void FirstRun()
        {
            foreach (var task in _TaskFactory.GetAllTasks())
                StartTask(task);
        }

        private void StartTask(ITask task)
        {
            Task.Factory
                .StartNew(() => task.Execute())
                .ContinueWith(t => FinishTask(task));
        }

        private void FinishTask(ITask task)
        {
            var TaskType = task.GetType();
            var NextTime = task.NextExecutionTime();
            _TaskFactory.DestroyTask(task);

            var Timer = new Timer();            
            Timer.Elapsed += (o, e) =>
                {
                    Timer.Dispose();
                    CreateTask(TaskType);
                };
            Timer.Interval = NextTime.TotalMilliseconds;
            Timer.Start();
        }

        private void CreateTask(Type TaskType)
        {
            var Task = _TaskFactory.GetTask(TaskType.Name);
            if (Task != null)
                StartTask(Task);            
        }
    }
}
