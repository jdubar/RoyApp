using RoyApp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RoyApp.Services
{
    public class ListviewService : IListviewService
    {
        public string[] GetHeaderList(ListView listView)
        {
            return listView.Columns
                        .OfType<ColumnHeader>()
                        .Select(header => header.Text.Trim())
                        .ToArray();
        }

        public List<List<string>> GetItemList(ListView listView)
        {
            List<List<string>> list = new List<List<string>>();
            for (int i = 0; i < listView.Items.Count; i++)
            {
                List<string> subs = new List<string>();
                for (int j = 0; j < listView.Items[i].SubItems.Count; j++)
                {
                    subs.Add(listView.Items[i].SubItems[j].Text);
                }
                list.Add(subs);
            }
            return list;
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
            listView.Items.Add(listViewItem);
        }

        public void ColumnSetAutoAdjust(ListView listView, int col)
        {
            // auto-adjust the ID column width based on text
            listView.Columns[col].Width = -1;
        }
    }
}
