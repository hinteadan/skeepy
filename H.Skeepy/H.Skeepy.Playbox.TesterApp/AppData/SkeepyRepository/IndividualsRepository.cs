using H.Skeepy.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Playbox.TesterApp.AppData.SkeepyRepository
{
    internal static class IndividualsRepository
    {
        private static readonly ConcurrentDictionary<string, Individual> individualsDictionary = new ConcurrentDictionary<string, Individual>();

        public static IEnumerable<Individual> All
        {
            get
            {
                return individualsDictionary.Values;
            }
        }

        public static void Save(Individual guy)
        {
            individualsDictionary.AddOrUpdate(guy.Id, guy, (x, y) => guy);
        }

        public static void Zap(string id)
        {
            individualsDictionary.TryRemove(id, out var guy);
        }
        public static void Zap(Individual guy)
        {
            Zap(guy.Id);
        }


        public static Individual ById(string id)
        {
            if (!individualsDictionary.ContainsKey(id))
            {
                throw new InvalidOperationException("The individual does not exist");
            }
            return individualsDictionary[id];
        }
    }
}
