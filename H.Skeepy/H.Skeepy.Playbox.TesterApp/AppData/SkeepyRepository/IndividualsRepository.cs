﻿using H.Skeepy.Model;
using System;
using System.Collections.Concurrent;
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
    public static class IndividualsRepository
    {
        private static ObservableCollection<Individual> individuals = new ObservableCollection<Individual>();
        private static readonly FileInfo individualsFile;

        static IndividualsRepository()
        {
            individualsFile = new FileInfo(ConfigurationManager.AppSettings["Skeepy.Data.IndividualsFilePath"] ?? "AppData/SkeepyRepository/Individuals.csv");
            var watcher = new FileSystemWatcher(individualsFile.DirectoryName, individualsFile.Name);
            watcher.Changed += (sender, e) =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    try { Refresh(); } catch (Exception) { }
                }));
            };
            watcher.EnableRaisingEvents = true;
        }

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
            if (!File.Exists(individualsFile.FullName))
            {
                return Enumerable.Empty<Individual>();
            }

            return File.ReadAllLines(individualsFile.FullName)
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
