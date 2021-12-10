using RoyApp.Interfaces;
using System;
using System.IO;

namespace RoyApp.Wrappers
{
    internal class FileIoWrapper : IFileService
    {
        private const string SEPARATOR = ",";

        public bool Exists(string filePath)
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
