using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure
{
    public static class Comparer
    {
        public static IEqualityComparer<T> For<T>(Func<T, T, bool> Comparer, Func<T, int> HashCodeCalculator)
        {
            return new Comparer<T>(Comparer, HashCodeCalculator);
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> self, Func<T, T, bool> Comparer, Func<T, int> HashCodeCalculator)
        {
            return self.Distinct<T>(For<T>(Comparer, HashCodeCalculator));
        }
    }

    public class Comparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparer;
        private readonly Func<T, int> _HashCodeCalculator;

        public Comparer(Func<T, T, bool> comparer, Func<T, int> HashCodeCalculator)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            _comparer = comparer;
            _HashCodeCalculator = HashCodeCalculator;
        }

        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _HashCodeCalculator(obj);
        }
    }
}
