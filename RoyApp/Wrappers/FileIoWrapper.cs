﻿using RoyApp.Interfaces;
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

        public bool WriteData(string filePath, string dataLine)
        {
            try
            {
                using StreamWriter writer = File.AppendText(filePath);
                writer.WriteLine(dataLine);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WriteHeader(string filePath, string[] dataLine)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine(string.Join(SEPARATOR, dataLine));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
