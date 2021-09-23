using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RoyApp.Services
{
    public interface IListviewService
    {
        void DeleteSelectedItems(object sender);
        string[] GetHeaderList(ListView listView);
        List<List<string>> GetItemList(ListView listView);
    }

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
    }
}
