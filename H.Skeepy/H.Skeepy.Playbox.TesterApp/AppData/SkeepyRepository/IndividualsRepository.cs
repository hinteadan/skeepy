using H.Skeepy.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Playbox.TesterApp.AppData.SkeepyRepository
{
    public static class IndividualsRepository
    {
        private static ObservableCollection<Individual> individuals = new ObservableCollection<Individual>();

        public static ObservableCollection<Individual> All
        {
            get
            {
                if (!individuals.Any())
                {
                    Refresh();
                }
                return individuals;
            }
        }

        public static void Refresh()
        {
            individuals.Clear();
            foreach (var guy in ReadAndParseIndividualsCsv())
            {
                individuals.Add(guy);
            }
        }

        private static IEnumerable<Individual> ReadAndParseIndividualsCsv()
        {
            var path = ConfigurationManager.AppSettings["Skeepy.Data.IndividualsFilePath"] ?? "AppData/SkeepyRepository/Individuals.csv";

            if (!File.Exists(path))
            {
                return Enumerable.Empty<Individual>();
            }

            return File.ReadAllLines(path)
                .Select(ParseIndividualFromCsvLine);
        }

        private static Individual ParseIndividualFromCsvLine(string csvLine)
        {
            var parts = csvLine.Split(',', '\t');
            if (parts.Length < 3)
            {
                throw new InvalidOperationException($"Invalid Individuals CSV for line: \"{csvLine}\"");
            }
            return Individual.Existing(parts[0], parts[1], parts[2]);
        }
    }
}
