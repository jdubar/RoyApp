using RoyApp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoyApp.Services
{
    public class ListviewService : IListviewService
    {
        private const string SEPARATOR = ",";
        public string[] GetHeaderList(ListView listView)
        {
            return listView.Columns
                        .OfType<ColumnHeader>()
                        .Select(header => header.Text.Trim())
                        .ToArray();
        }

        public string GetItemList(ListView listView)
        {
            var allItems = new StringBuilder("");
            for (var i = 0; i < listView.Items.Count; i++)
            {
                var lineOfItems = new StringBuilder("");
                for (var j = 0; j < listView.Items[i].SubItems.Count; j++)
                {
                    _ = lineOfItems.Append($"{listView.Items[i].SubItems[j].Text}{SEPARATOR}");
                }
                _ = allItems.AppendLine(lineOfItems.ToString());
            }
            return allItems.ToString();
        }

        public void DeleteSelectedItems(object sender)
        {
            foreach (ListViewItem item in ((ListView)sender).SelectedItems)
            {
                item.Remove();
            }
        }

        public void AddItemToListView(ListView listView, string[] row)
        {
            var listViewItem = new ListViewItem(row);
            _ = listView.Items.Add(listViewItem);
        }

        public void AddListOfItemsToListView(ListView listView, List<string[]> dataList)
        {
            foreach (var row in dataList)
            {
                AddItemToListView(listView, row);
            }
        }

        public void ColumnSetAutoAdjust(ListView listView, int col)
        {
            // Auto-adjust the ID column width based on text
            listView.Columns[col].Width = -1;
        }
    }
}
