using RoyApp.interfaces;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RoyApp
{
    public static class ListviewActions
    {
        public static void ListViewToCSV(ListView listView, string filePath, bool includeHidden)
        {
            if (listView is null)
            {
                throw new ArgumentNullException(nameof(listView));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or empty.", nameof(filePath));
            }
            //make header string
            StringBuilderService result = new StringBuilderService();
            WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listView.Columns[i].Text);

            //export data rows
            foreach (ListViewItem listItem in listView.Items)
                WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listItem.SubItems[i].Text);

            File.WriteAllText(filePath, result.ToString());
        }

        public static void WriteCSVRow(StringBuilderService result, int itemsCount, Func<int, bool> isColumnNeeded, Func<int, string> columnValue)
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
    }
}
