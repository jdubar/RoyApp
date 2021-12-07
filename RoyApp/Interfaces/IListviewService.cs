using System.Collections.Generic;
using System.Windows.Forms;

namespace RoyApp.Interfaces
{
    public interface IListviewService
    {
        void AddItemToListView(ListView listView, string[] row);
        void ColumnSetAutoAdjust(ListView listView, int col);
        void DeleteSelectedItems(object sender);
        string[] GetHeaderList(ListView listView);
        List<List<string>> GetItemList(ListView listView);
    }
}
