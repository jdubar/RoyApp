using RoyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoyApp.Services
{
    public class FileService
    {
        private readonly IFileService _fileService;

        public FileService(IFileService fileService) => _fileService = fileService;

        public List<string[]> ReadImportData(string filePath)
        {
            return _fileService.ReadImportData(filePath);
        }

        public void WriteDataToFile(string filePath, string[] headers, string itemList)
        {
            if (headers?.Length != 6)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            if (string.IsNullOrEmpty(itemList))
            {
                throw new ArgumentNullException(nameof(itemList));
            }

            _fileService.WriteHeader(filePath, headers);
            _fileService.WriteData(filePath, itemList);
        }

        public bool Exists(string filePath)
        {
            return _fileService.Exists(filePath);
        }

        public bool WriteData(string filePath, string dataLine)
        {
            return _fileService.WriteData(filePath, dataLine);
        }

        public bool WriteHeader(string filePath, string[] dataLine)
        {
            return _fileService.WriteHeader(filePath, dataLine);
        }
    }
}
