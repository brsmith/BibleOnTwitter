using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Web;

namespace BibleOnTwitter.Infrastructure.DataAccess
{
    public class HttpNHibernateSessionProvider : INHibernateSessionProvider, IDisposable
    {
        private readonly ISessionFactory _SessionFactory;

        public HttpNHibernateSessionProvider(ISessionFactory SessionFactory)
        {
            _SessionFactory = SessionFactory;
        }

        public ISession GetOrCreateSession()
        {
            return HandleHttpSession("NHibernateSession", () => _SessionFactory.OpenSession());
        }

        public IStatelessSession GetOrCreateStatelessSession()
        {
            return HandleHttpSession("NHibernateStatelessSession", () => _SessionFactory.OpenStatelessSession());
        }

        public void DestroyCurrentSession()
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("No current HttpContext exists");

            var Session = HttpContext.Current.Session["NHibernateSession"] as ISession;
            if (Session != null)
            {
                Session.Flush();
                Session.Dispose();
                HttpContext.Current.Session.Remove("NHibernateSession");
            }

            var StatelessSession = HttpContext.Current.Session["NHibernateStatelessSession"] as IStatelessSession;
            if (StatelessSession != null)
            {
                StatelessSession.Dispose();
                HttpContext.Current.Session.Remove("NHibernateStatelessSession");
            }
        }

        private T HandleHttpSession<T>(string Key, Func<T> Builder) where T : class
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("No current HttpContext exists");

            var Result = HttpContext.Current.Session[Key];
            if (Result == null)
            {
                Result = Builder();
                HttpContext.Current.Session[Key] = Result;
            }
            return Result as T;            
        }

        public void Dispose()
        {
            this.DestroyCurrentSession();
        }
    }
}
