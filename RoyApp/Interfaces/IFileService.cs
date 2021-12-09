using System.Collections.Generic;

namespace RoyApp.Interfaces
{
    public interface IFileService
    {
        void WriteToCsv(List<List<string>> itemList, string[] headers, string filePath);
    }
}
