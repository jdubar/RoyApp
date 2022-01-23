using System.Collections.Generic;
using System.Windows.Forms;

namespace RoyApp.Interfaces
{
    public interface IListviewService
    {
        void AddItemToListView(ListView listView, string[] row);
        void AddListOfItemsToListView(ListView listView, List<string[]> dataList);
        void ColumnSetAutoAdjust(ListView listView, int col);
        void DeleteSelectedItems(object sender);
        string[] GetHeaderList(ListView listView);
        string GetItemList(ListView listView);
    }
}
