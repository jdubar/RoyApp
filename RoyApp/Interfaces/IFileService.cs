using System.Collections.Generic;

namespace RoyApp.Interfaces
{
    public interface IFileService
    {
        bool Exists(string filePath);
        List<string[]> ReadImportData(string filePath);
        bool WriteData(string filePath, string dataLine);
        bool WriteHeader(string filePath, string[] dataLine);
    }
}
