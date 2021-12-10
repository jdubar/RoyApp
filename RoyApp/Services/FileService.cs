using RoyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoyApp.Services
{
    public class FileService
    {
        private readonly IFileService _fileService;
        private const string SEPARATOR = ",";

        public FileService(IFileService fileService) => _fileService = fileService;

        public void WriteDataToFile(string filePath, string[] headers, List<List<string>> itemList)
        {
            if (!_fileService.Exists(filePath))
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

            _fileService.WriteLine(filePath, headers);
            itemList.ForEach(line =>
            {
                _fileService.WriteLine(filePath,
                    line.Select(c => c.Contains(SEPARATOR) ? c.Replace(SEPARATOR.ToString(), "\\" + SEPARATOR) : c).ToArray()
                    );
            });
        }

        public bool Exists(string filePath)
        {
            return _fileService.Exists(filePath);
        }

        public bool WriteLine(string filePath, string[] dataLine)
        {
            return _fileService.WriteLine(filePath, dataLine);
        }
    }
}
