using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Skeepy.Playbox.TesterApp.AppData.SkeepyRepository
{
    public static class TeamsRepository
    {
        private static ObservableCollection<Party> parties = new ObservableCollection<Party>();
        private static readonly FileInfo teamsFile;

        static TeamsRepository()
        {
            teamsFile = new FileInfo(ConfigurationManager.AppSettings["Skeepy.Data.TeamsFilePath"] ?? "AppData/SkeepyRepository/Teams.csv");
            var watcher = new FileSystemWatcher(teamsFile.DirectoryName, teamsFile.Name);
            watcher.Changed += (sender, e) =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    try { Refresh(); } catch (Exception) { }
                }));
            };
            watcher.EnableRaisingEvents = true;
        }

        public static ObservableCollection<Party> All
        {
            get
            {
                if (!parties.Any())
                {
                    Refresh();
                }
                return parties;
            }
        }

        private static void Refresh()
        {
            parties.Clear();
            foreach (var party in ReadAndParsePartiesCsv())
            {
                parties.Add(party);
            }
            foreach(var player in IndividualsRepository.All)
            {
                parties.Add(player.ToParty());
            }
        }

        private static IEnumerable<Party> ReadAndParsePartiesCsv()
        {
            if (!File.Exists(teamsFile.FullName))
            {
                return Enumerable.Empty<Party>();
            }

            return File.ReadAllLines(teamsFile.FullName)
                .Select(ParsePartyFromCsvLine);
        }

        private static Party ParsePartyFromCsvLine(string csvLine)
        {
            var parts = csvLine.Split(',', '\t');
            if (parts.Length < 3)
            {
                throw new InvalidOperationException($"Invalid Party CSV for line: \"{csvLine}\"");
            }
            return Party.Existing(parts[0], parts[1], parts.Skip(2).Select(IndividualsRepository.ById).OrderBy(x => x.FullName).ToArray());
        }
    }
}
