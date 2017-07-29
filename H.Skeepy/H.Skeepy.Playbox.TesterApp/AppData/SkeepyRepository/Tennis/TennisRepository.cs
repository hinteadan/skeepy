using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Playbox.TesterApp.AppData.SkeepyRepository.Tennis
{
    public static class TennisRepository
    {
        private static readonly FileInfo wtaPlayersFile;
        private static readonly FileInfo atpPlayersFile;
        private static readonly FileInfo wtaRankingsFile;
        private static readonly FileInfo atpRankingsFile;

        //The columns for the ranking files are: ranking_date, ranking, player_id, ranking_points, tours.
        //The player file columns are: player_id, first_name, last_name, hand, birth_date, country_code.

        private static Individual[] wtaPlayers;
        private static Individual[] atpPlayers;
        private static Individual[] allPlayers;

        static TennisRepository()
        {
            wtaPlayersFile = new FileInfo(ConfigurationManager.AppSettings["Skeepy.Data.Tennis.WtaPlayersFilePath"] ?? "AppData/SkeepyRepository/Tennis/wta_players.csv");
            atpPlayersFile = new FileInfo(ConfigurationManager.AppSettings["Skeepy.Data.Tennis.AtpPlayersFilePath"] ?? "AppData/SkeepyRepository/Tennis/atp_players.csv");
            wtaRankingsFile = new FileInfo(ConfigurationManager.AppSettings["Skeepy.Data.Tennis.WtaRankingsFilePath"] ?? "AppData/SkeepyRepository/Tennis/wta_rankings_current.csv");
            atpRankingsFile = new FileInfo(ConfigurationManager.AppSettings["Skeepy.Data.Tennis.AtpRankingsFilePath"] ?? "AppData/SkeepyRepository/Tennis/atp_rankings_current.csv");
        }

        public static Individual[] Wta { get { return wtaPlayers; } }
        public static Individual[] Atp { get { return atpPlayers; } }
        public static Individual[] All { get { return allPlayers; } }

        public static void Refresh(int top = 50)
        {
            wtaPlayers = ParsePlayersCsv(wtaPlayersFile, ParseRankingsCsv(wtaRankingsFile, top));
            atpPlayers = ParsePlayersCsv(atpPlayersFile, ParseRankingsCsv(atpRankingsFile, top));
            allPlayers = wtaPlayers.Concat(atpPlayers).OrderBy(x => x.FullName).ToArray();
        }

        private static Individual[] ParsePlayersCsv(FileInfo playersFile, RankingInfo[] rankingInfo)
        {
            if (!playersFile.Exists) return new Individual[0];
            return File
                .ReadAllLines(playersFile.FullName)
                .Where(x => rankingInfo.Any(r => x.StartsWith(r.PlayerId)))
                .Select(ParsePlayersCsvLine)
                .Where(x => x != null)
                .OrderBy(x => x.FullName)
                .ToArray();
        }

        private static Individual ParsePlayersCsvLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return null;
            var parts = line.Split(',');
            if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[1])) return null;

            var player = Individual.New(parts[1], parts.Length > 2 ? parts[2] : null);
            player.SetDetail("OriginalId", parts[0]);
            if (parts.Length > 3 && !string.IsNullOrWhiteSpace(parts[3])) player.SetDetail("Hand", parts[3]);
            if (parts.Length > 4 && !string.IsNullOrWhiteSpace(parts[4])) player.SetDetail("Birthday", parts[4]);
            if (parts.Length > 5 && !string.IsNullOrWhiteSpace(parts[5])) player.SetDetail("Country", parts[5]);

            return player;
        }

        private static RankingInfo[] ParseRankingsCsv(FileInfo rankingsFile, int top)
        {
            if (!rankingsFile.Exists) return new RankingInfo[0];

            return File
                .ReadAllLines(rankingsFile.FullName)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(RankingInfo.Parse)
                .OrderBy(x => x.Ranking)
                .ThenByDescending(x => x.RankingDate)
                .GroupBy(x => x.PlayerId)
                .Take(top)
                .Select(x => x.First())
                .ToArray();
        }

        private class RankingInfo
        {
            public string RankingDate { get; set; }
            public int Ranking { get; set; }
            public string PlayerId { get; set; }
            public string RankingPoints { get; set; }
            public string Tours { get; set; }

            public static RankingInfo Parse(string csvLine)
            {
                var parts = csvLine.Split(',');
                return new RankingInfo
                {
                    RankingDate = parts[0],
                    Ranking = int.Parse(parts[1]),
                    PlayerId = parts[2],
                    RankingPoints = parts[3],
                    Tours = parts.Length > 4 ? parts[4] : null,
                };
            }
        }
    }
}
