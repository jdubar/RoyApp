namespace RoyApp.Interfaces
{
    public interface IFileService
    {
        bool Exists(string filePath);
        void WriteLine(string filePath, string[] dataLine);
    }
}
