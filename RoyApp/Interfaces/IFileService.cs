using System.Collections.Generic;

namespace RoyApp.Interfaces
{
    public interface IFileService
    {
        bool IsFileExists(string filePath);
        void WriteDataToFile(string filePath, string[] headers, List<List<string>> itemList);
        void WriteLine(string filePath, string[] dataLine);
    }
}
