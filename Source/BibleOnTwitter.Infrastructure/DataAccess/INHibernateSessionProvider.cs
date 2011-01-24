using System;
using NHibernate;

namespace BibleOnTwitter.Infrastructure.DataAccess
{
    public interface INHibernateSessionProvider
    {
        void DestroyCurrentSession();
        ISession GetOrCreateSession();
        IStatelessSession GetOrCreateStatelessSession();
    }
}
