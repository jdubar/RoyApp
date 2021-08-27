using RoyApp.interfaces;
using System;
using System.IO;

namespace RoyApp.Services
{
    public class FileService
    {
        public static void WriteCSVRow(IStringBuilderService result, int itemsCount, Func<int, bool> isColumnNeeded, Func<int, string> columnValue)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            bool isFirstTime = true;
            for (int i = 0; i < itemsCount; i++)
            {
                if (!isColumnNeeded(i))
                    continue;

                if (!isFirstTime)
                    result.Append(",");
                isFirstTime = false;

                result.Append(String.Format("\"{0}\"", columnValue(i)));
            }
            result.AppendLine();
        }

        public static void WriteCSVText(string filePath, IStringBuilderService result)
        {
            File.WriteAllText(filePath, result.ToString());
        }
    }
}
