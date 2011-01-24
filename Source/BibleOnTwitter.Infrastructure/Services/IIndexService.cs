using System;
using BibleOnTwitter.Infrastructure.Model.View;

namespace BibleOnTwitter.Infrastructure.Services
{
    public interface IIndexService
    {
        IndexView GetIndexView();
    }
}
