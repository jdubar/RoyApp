using RoyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RoyApp.Services
{
    public class FileService : IFileService
    {
        private const string SEPARATOR = ",";

        public void WriteDataToFile(string filePath, string[] headers, List<List<string>> itemList)
        {
            if (!IsFileExists(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or empty.", nameof(filePath));
            }

            if (itemList?.Any() != true)
            {
                throw new ArgumentNullException(nameof(itemList));
            }

            if (headers?.Any() != true)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            WriteLine(filePath, headers);
            itemList.ForEach(line =>
            {
                WriteLine(filePath,
                    line.Select(c => c.Contains(SEPARATOR) ? c.Replace(SEPARATOR.ToString(), "\\" + SEPARATOR) : c).ToArray()
                    );
            });
        }

        public bool IsFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void WriteLine(string filePath, string[] dataLine)
        {
            using StreamWriter writer = new StreamWriter(filePath);
            writer.WriteLine(string.Join(SEPARATOR, dataLine) + Environment.NewLine);
        }
    }
}
