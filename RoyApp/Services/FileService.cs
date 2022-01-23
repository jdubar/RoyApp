using RoyApp.Interfaces;
using System;
using System.Linq;

namespace RoyApp.Services
{
    public class FileService
    {
        private readonly IFileService _fileService;

        public FileService(IFileService fileService) => _fileService = fileService;

        public void WriteDataToFile(string filePath, string[] headers, string itemList)
        {
            if (itemList?.Any() != true)
            {
                throw new ArgumentNullException(nameof(itemList));
            }

            if (headers?.Any() != true)
            {
                throw new ArgumentNullException(nameof(headers));
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
