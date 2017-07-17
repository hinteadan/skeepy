using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model
{
    public class DetailsHolder
    {
        protected readonly ConcurrentDictionary<string, string> detailsDictionary = new ConcurrentDictionary<string, string>();

        public Dictionary<string, string> Details
        {
            get
            {
                return detailsDictionary.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public DetailsHolder SetDetail(string key, string value)
        {
            detailsDictionary.AddOrUpdate(key, value, (k, old) => value);
            return this;
        }

        public string GetDetail(string key)
        {
            return detailsDictionary.ContainsKey(key) ? detailsDictionary[key] : string.Empty;
        }

        public bool HasDetail(string key)
        {
            return detailsDictionary.ContainsKey(key);
        }

        public DetailsHolder ZapDetail(string key)
        {
            detailsDictionary.TryRemove(key, out string none);
            return this;
        }
    }
}
