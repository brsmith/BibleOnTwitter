using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace BibleOnTwitter.Infrastructure.DataAccess
{
    public static class INHibernateSessionProviderExtensions 
    {
        public static void Transactional(this INHibernateSessionProvider self, Action<ISession> Work)
        {
            var Session = self.GetOrCreateSession();
            using (var Transaction = Session.BeginTransaction())
            {
                try
                {
                    Work(Session);
                    Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    throw new Exception("An exception occurred while trying execute a transaction", ex);
                }
            }
        }
    }
}
