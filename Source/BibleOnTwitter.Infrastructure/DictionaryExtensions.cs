using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleOnTwitter.Infrastructure
{
    public static class DictionaryExtensions
    {
        public static TValue AssuredGet<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey Key, Func<TValue> Builder)
        {
            if (self == null)
                throw new NullReferenceException();

            if (self.ContainsKey(Key))
                return self[Key];

            var NewValue = Builder();
            self[Key] = NewValue;
            return NewValue;
        }
    }
}
