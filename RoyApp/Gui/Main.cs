using RoyApp.Services;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RoyApp
{
    public partial class Main : Form
    {
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;
        private readonly IListviewService _listviewService;

        public Main(IDataService dataService, IFileService fileService, IListviewService listviewService)
        {
            InitializeComponent();
            _dataService = dataService;
            _fileService = fileService;
            _listviewService = listviewService;
        }

        private string BedtimeIdInForm
        {
            get => bedtimeId.Text;
            set => bedtimeId.Text = value;
        }
        private string BedtimeEnteredInForm
        {
            get => bedtimeEntered.Text;
            set => bedtimeEntered.Text = value;
        }
        private string BedtimeDecInForm
        {
            get => bedtimeDec.Text;
            set => bedtimeDec.Text = value;
        }
        private string BedtimeAvgInForm
        {
            get => bedtimeAvg.Text;
            set => bedtimeAvg.Text = value;
        }
        private string WaketimeEnteredInForm
        {
            get => waketimeEntered.Text;
            set => waketimeEntered.Text = value;
        }
        private string WaketimeDecInForm
        {
            get => waketimeDec.Text;
            set => waketimeDec.Text = value;
        }
        private string WaketimeAvgInForm
        {
            get => waketimeAvg.Text;
            set => waketimeAvg.Text = value;
        }

        private void BedTime_TextChanged(object sender, EventArgs e)
        {
            BedtimeDecInForm = _dataService.TimeToDecimal(BedtimeEnteredInForm).ToString();
        }

        private void Waketime_TextChanged(object sender, EventArgs e)
        {
            WaketimeDecInForm = _dataService.TimeToDecimal(WaketimeEnteredInForm).ToString();
        }

        public void ButtonAdd_Click(object sender, EventArgs e)
        {
            string duration = _dataService.TimeDuration(BedtimeDecInForm, WaketimeDecInForm).ToString();
            if (duration == "-1")
            {
                MessageBox.Show("Please enter a value");
                return;
            }

            string[] row = { BedtimeIdInForm, BedtimeEnteredInForm, BedtimeDecInForm, WaketimeEnteredInForm, WaketimeDecInForm, duration };
            var listViewItem = new ListViewItem(row);
            listViewDataList.Items.Add(listViewItem);

            decimal bedtimeTotal = 0;
            decimal waketimeTotal = 0;

            var collection = listViewDataList.Items;
            foreach (var item in collection)
            {
                bedtimeTotal += Convert.ToDecimal(BedtimeDecInForm);
                waketimeTotal += Convert.ToDecimal(WaketimeDecInForm);
            }

            BedtimeAvgInForm = _dataService.TimeAverage(bedtimeTotal, listViewDataList.Items.Count).ToString();
            WaketimeAvgInForm = _dataService.TimeAverage(waketimeTotal, listViewDataList.Items.Count).ToString();
            // auto-adjust the ID column width based on text
            listViewDataList.Columns[0].Width = -1;
            ClearTextData();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void ClearAllData()
        {
            listViewDataList.Items.Clear();
            ClearAverages();
            ClearTextData();
        }

        private void ClearAverages()
        {
            BedtimeAvgInForm = "";
            WaketimeAvgInForm = "";
        }

        private void ClearTextData()
        {
            BedtimeIdInForm = "";
            BedtimeEnteredInForm = "";
            BedtimeDecInForm = "";
            WaketimeEnteredInForm = "";
            WaketimeDecInForm = "";
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            var filePath = ShowSaveDialog();
            if (filePath != null)
            {
                try
                {
                    var headers = listViewDataList.Columns
                                  .OfType<ColumnHeader>()
                                  .Select(header => header.Text.Trim())
                                  .ToArray();

                    var items = _listviewService.GetItemList(listViewDataList);
                    _fileService.WriteToCsv(items, headers, filePath);
                    MessageBox.Show("File successfully exported!",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, 
                                    "Error", 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Error);
                }
            }
        }

        private Stream ShowOpenDialog()
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
                                 openFileDialog.InitialDirectory = "";
                                 openFileDialog.Filter = "csv files (*.csv)|*.csv";
                                 openFileDialog.FilterIndex = 2;
                                 openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.OpenFile();
            }
            return null;
        }

        private string ShowSaveDialog()
        {
            using SaveFileDialog exportFileDialog = new SaveFileDialog();
                                 exportFileDialog.Filter = "csv files (*.csv)|*.csv";
                                 exportFileDialog.FilterIndex = 2;
                                 exportFileDialog.RestoreDirectory = true;

            Stream myStream;
            if ((exportFileDialog.ShowDialog() == DialogResult.OK) && (myStream = exportFileDialog.OpenFile()) != null)
            {
                myStream.Close();
                return exportFileDialog.FileName;
            }
            return null;
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            var fileStream = ShowOpenDialog();
            if (fileStream != null)
            {
                ClearAllData();

                using StreamReader reader = new StreamReader(fileStream);
                // skip the header line
                reader.ReadLine();
                string currentLine;

                decimal bedtimeTotal = 0;
                decimal waketimeTotal = 0;
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = reader.ReadLine()) != null)
                {
                    string[] cols = currentLine.Split(',');
                    // 0 - id, 1 - bedtime raw, 2 - waketime raw
                    string[] row = {
                        cols[0].Trim('"'),
                        cols[1].Trim('"'),
                        _dataService.TimeToDecimal(cols[1].Trim('"')).ToString(),
                        cols[2].Trim('"'),
                        _dataService.TimeToDecimal(cols[2].Trim('"')).ToString(),
                        _dataService.TimeDuration(_dataService.TimeToDecimal(cols[1].Trim('"')).ToString(), _dataService.TimeToDecimal(cols[2].Trim('"')).ToString()).ToString()
                    };
                    var listViewItem = new ListViewItem(row);
                    listViewDataList.Items.Add(listViewItem);
                    bedtimeTotal += Convert.ToDecimal(row[2]);
                    waketimeTotal += Convert.ToDecimal(row[4]);
                }

                BedtimeAvgInForm = _dataService.TimeAverage(bedtimeTotal, listViewDataList.Items.Count).ToString();
                WaketimeAvgInForm = _dataService.TimeAverage(waketimeTotal, listViewDataList.Items.Count).ToString();
                // auto-adjust the ID column width based on text
                listViewDataList.Columns[0].Width = -1;
            }
        }

        private void DataList_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                _listviewService.DeleteSelectedItems(sender);
            }
        }
    }
}
