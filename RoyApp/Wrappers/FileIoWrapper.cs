using RoyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace RoyApp.Wrappers
{
    internal class FileIoWrapper : IFileService
    {
        private readonly IDataService _dataService;

        public FileIoWrapper(IDataService dataService) => _dataService = dataService;

        private const string SEPARATOR = ",";

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public List<string[]> ReadImportData(string filePath)
        {
            List<string[]> allLines = new List<string[]>();
            using StreamReader reader = new StreamReader(filePath);
            // skip the header line
            reader.ReadLine();
            string currentLine;

            // currentLine will be null when the StreamReader reaches the end of file
            while ((currentLine = reader.ReadLine()) != null)
            {
                allLines.Add(_dataService.SplitLineData(currentLine));
            }
            return allLines;
        }

        public bool WriteData(string filePath, string dataLine)
        {
            try
            {
                using StreamWriter writer = File.AppendText(filePath);
                writer.WriteLine(dataLine);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WriteHeader(string filePath, string[] dataLine)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine(string.Join(SEPARATOR, dataLine));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
