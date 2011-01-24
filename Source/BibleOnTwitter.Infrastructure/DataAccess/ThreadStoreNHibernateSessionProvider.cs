using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NHibernate;

namespace BibleOnTwitter.Infrastructure.DataAccess
{
    public class ThreadStoreNHibernateSessionProvider : INHibernateSessionProvider
    {
        [ThreadStatic]
        private static ISession _Session;

        [ThreadStatic]
        private static IStatelessSession _StatelessSession;

        private readonly ISessionFactory _SessionFactory;

        public ThreadStoreNHibernateSessionProvider(ISessionFactory SessionFactory)
        {
            _SessionFactory = SessionFactory;
        }

        public ISession GetOrCreateSession()
        {
            if (_Session == null)
                _Session = _SessionFactory.OpenSession();

            return _Session;
        }

        public IStatelessSession GetOrCreateStatelessSession()
        {
            if (_StatelessSession == null)
                _StatelessSession = _SessionFactory.OpenStatelessSession();

            return _StatelessSession;
        }

        public void DestroyCurrentSession()
        {
            if (_Session != null)
            {
                _Session.Flush();
                _Session.Dispose();
                _Session = null;
            }

            if (_StatelessSession != null)
            {
                _StatelessSession.Dispose();
                _StatelessSession = null;
            }
        }
    }
}
