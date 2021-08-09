using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace RoyApp
{
    public partial class GUI_Main : Form
    {
        public GUI_Main()
        {
            InitializeComponent();
        }

        private void bedTime_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            bedtimeDec.Text = TimeToData.TimeToDecimal(tb.Text).ToString();
        }

        private void waketime_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            waketimeDec.Text = TimeToData.TimeToDecimal(tb.Text).ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            double bedtimeTotal = 0;
            double waketimeTotal = 0;

            string duration = TimeToData.TimeDuration(bedtimeDec.Text, waketimeDec.Text).ToString();
            string[] row = { bedtimeId.Text, bedtime.Text, bedtimeDec.Text, waketime.Text, waketimeDec.Text, duration };
            var listViewItem = new ListViewItem(row);
            DataList.Items.Add(listViewItem);

            List<ListItems> templist = new List<ListItems>();

            var collection = DataList.Items;
            foreach (var item in collection)
            {
                ListViewItem obj = (ListViewItem)item;
                var subitems = obj.SubItems;
                List<string> stringlist = new List<string>();

                foreach (ListViewSubItem subitem in subitems)
                {
                    stringlist.Add(subitem.Text);
                }

                ListItems tempobject = new ListItems()
                {
                    listId = stringlist[0],
                    bedtimeRaw = stringlist[1],
                    bedtimeRec = stringlist[2],
                    waketimeRaw = stringlist[3],
                    waketimeRec = stringlist[4],
                    duration = stringlist[5]

                };
                templist.Add(tempobject);
                bedtimeTotal += Convert.ToDouble(stringlist[2]);
                waketimeTotal += Convert.ToDouble(stringlist[4]);

            }

            bedtimeAvg.Text = TimeToData.TimeAverage(bedtimeTotal, DataList.Items.Count).ToString();
            waketimeAvg.Text = TimeToData.TimeAverage(waketimeTotal, DataList.Items.Count).ToString();

            ClearTextData();

            void ClearTextData()
            {
                bedtimeId.Text = "";
                bedtime.Text = "";
                bedtimeDec.Text = "";
                waketime.Text = "";
                waketimeDec.Text = "";
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            DataList.Items.Clear();
            bedtimeAvg.Text = "";
            waketimeAvg.Text = "";
        }

        private void DataList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = DataList.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                bedtimeId.Text = DataList.SelectedItems[0].SubItems[0].Text;
                bedtime.Text = DataList.SelectedItems[0].SubItems[1].Text;
                bedtimeDec.Text = DataList.SelectedItems[0].SubItems[2].Text;
                waketime.Text = DataList.SelectedItems[0].SubItems[3].Text;
                waketimeDec.Text = DataList.SelectedItems[0].SubItems[4].Text;
            }
        }
    }
}
