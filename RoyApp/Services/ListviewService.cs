using RoyApp.interfaces;
using System;
using System.Windows.Forms;

namespace RoyApp.Services
{
    public static class ListviewService
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
            IStringBuilderService result = new StringService.StringBuilderService();
            FileService.WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listView.Columns[i].Text);

            //export data rows
            foreach (ListViewItem listItem in listView.Items)
                FileService.WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listItem.SubItems[i].Text);

            FileService.WriteCSVText(filePath, result);
        }

    }
}
