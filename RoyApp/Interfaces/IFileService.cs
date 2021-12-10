namespace RoyApp.Interfaces
{
    public interface IFileService
    {
        bool Exists(string filePath);
        bool WriteLine(string filePath, string[] dataLine);
    }
}
