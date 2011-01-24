using System;

namespace BibleOnTwitter.Infrastructure.Services
{
    public interface ITaskService
    {
        void Start();
        void Stop();
    }
}
