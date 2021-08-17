﻿using System;
using System.IO;
using System.Windows.Forms;

namespace RoyApp
{
    public partial class GUI_Main : Form
    {
        public GUI_Main()
        {
            InitializeComponent();
        }

        private void BedTime_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            bedtimeDec.Text = TimeToData.TimeToDecimal(tb.Text).ToString();
        }

        private void Waketime_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            waketimeDec.Text = TimeToData.TimeToDecimal(tb.Text).ToString();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            double bedtimeTotal = 0;
            double waketimeTotal = 0;

            string duration = TimeToData.TimeDuration(bedtimeDec.Text, waketimeDec.Text).ToString();

            string[] row = { bedtimeId.Text, bedtime.Text, bedtimeDec.Text, waketime.Text, waketimeDec.Text, duration };
            var listViewItem = new ListViewItem(row);
            DataList.Items.Add(listViewItem);

            var collection = DataList.Items;
            foreach (var item in collection)
            {
                bedtimeTotal += Convert.ToDouble(bedtimeDec.Text);
                waketimeTotal += Convert.ToDouble(waketimeDec.Text);
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

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            DataList.Items.Clear();
            bedtimeAvg.Text = "";
            waketimeAvg.Text = "";
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog exportFileDialog = new SaveFileDialog
            {
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (exportFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = exportFileDialog.OpenFile()) != null)
                {
                    myStream.Close();
                    try
                    {
                        ListviewActions.ListViewToCSV(DataList, exportFileDialog.FileName, false);
                        MessageBox.Show("File successfully exported!");
                    }
                    catch
                    {
                        // Handle exception
                    }

                }
            }
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "";
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using StreamReader reader = new StreamReader(fileStream);
                // skip the header line
                reader.ReadLine();
                string currentLine;

                double bedtimeTotal = 0;
                double waketimeTotal = 0;
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = reader.ReadLine()) != null)
                {
                    string[] cols = currentLine.Split(',');
                    // 0 - id, 1 - bedtime raw, 2 - waketime raw
                    string[] row = { 
                        cols[0].Trim('"'), 
                        cols[1].Trim('"'), 
                        TimeToData.TimeToDecimal(cols[1].Trim('"')).ToString(), 
                        cols[2].Trim('"'), 
                        TimeToData.TimeToDecimal(cols[2].Trim('"')).ToString(), 
                        TimeToData.TimeDuration(TimeToData.TimeToDecimal(cols[1].Trim('"')).ToString(), TimeToData.TimeToDecimal(cols[2].Trim('"')).ToString()).ToString()
                    };
                    var listViewItem = new ListViewItem(row);
                    DataList.Items.Add(listViewItem);
                    bedtimeTotal += Convert.ToDouble(row[2]);
                    waketimeTotal += Convert.ToDouble(row[4]);
                }
                bedtimeAvg.Text = TimeToData.TimeAverage(bedtimeTotal, DataList.Items.Count).ToString();
                waketimeAvg.Text = TimeToData.TimeAverage(waketimeTotal, DataList.Items.Count).ToString();
            }
        }

        private void DataList_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem DataList in ((ListView)sender).SelectedItems)
                {
                    DataList.Remove();
                }
            }
        }
    }
}
