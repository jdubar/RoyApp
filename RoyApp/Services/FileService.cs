using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RoyApp.Services
{
    public interface IFileService
    {
        void WriteToCsv(List<List<string>> itemList, string[] headers, string filePath);
    }

    public class FileService : IFileService
    {
        public void WriteToCsv(List<List<string>> itemList, string[] headers, string filePath)
        {
            bool isNullOrEmpty = itemList?.Any() != true;
            if (isNullOrEmpty)
            {
                throw new ArgumentNullException(nameof(itemList));
            }
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or empty.", nameof(filePath));
            }

            const string SEPARATOR = ",";
            using StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine(string.Join(",", headers) + Environment.NewLine);
                itemList.ForEach(line =>
                {
                    var lineArray = line.Select(c =>
                        c.Contains(SEPARATOR) ? c.Replace(SEPARATOR.ToString(), "\\" + SEPARATOR) : c).ToArray();
                    writer.WriteLine(string.Join(SEPARATOR, lineArray));
                });
        }
    }
}
