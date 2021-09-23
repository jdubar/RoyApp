using RoyApp.Interfaces;
using RoyApp.Services;
using System;
using System.IO;
using System.Windows.Forms;

namespace RoyApp
{
    public partial class Main : Form, IMainFormData
    {
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;
        private readonly IListviewService _listviewService;
        private const string csvExt = "csv files (*.csv)|*.csv";

        public Main(IDataService dataService, IFileService fileService, IListviewService listviewService)
        {
            InitializeComponent();
            _dataService = dataService;
            _fileService = fileService;
            _listviewService = listviewService;
        }

        public string IdInForm
        {
            get => bedtimeId.Text;
            set => bedtimeId.Text = value;
        }

        public string BedtimeEnteredInForm
        {
            get => bedtimeEntered.Text;
            set => bedtimeEntered.Text = value;
        }

        public string BedtimeDecInForm
        {
            get => bedtimeDec.Text;
            set => bedtimeDec.Text = value;
        }

        public string WaketimeEnteredInForm
        {
            get => waketimeEntered.Text;
            set => waketimeEntered.Text = value;
        }

        public string WaketimeDecInForm
        {
            get => waketimeDec.Text;
            set => waketimeDec.Text = value;
        }

        public string BedtimeAvg
        {
            set => bedtimeAvg.Text = value;
        }

        public string WaketimeAvg
        {
            set => waketimeAvg.Text = value;
        }

        private void BedTime_TextChanged(object sender, EventArgs e) => BedtimeDecInForm = _dataService.TimeToDecimal(BedtimeEnteredInForm).ToString();

        private void Waketime_TextChanged(object sender, EventArgs e) => WaketimeDecInForm = _dataService.TimeToDecimal(WaketimeEnteredInForm).ToString();

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            string duration = _dataService.TimeDuration(BedtimeDecInForm, WaketimeDecInForm).ToString();
            if (duration == "-1")
            {
                MessageBox.Show("Please enter a value");
                return;
            }

            string[] row = { IdInForm, BedtimeEnteredInForm, BedtimeDecInForm, WaketimeEnteredInForm, WaketimeDecInForm, duration };
            var listViewItem = new ListViewItem(row);
            listViewDataList.Items.Add(listViewItem);

            CalculateTotals();
            // auto-adjust the ID column width based on text
            listViewDataList.Columns[0].Width = -1;
            ClearTextData();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            var filePath = ShowSaveDialog();
            if (filePath != null)
            {
                try
                {
                    _fileService.WriteToCsv(_listviewService.GetItemList(listViewDataList),
                                            _listviewService.GetHeaderList(listViewDataList),
                                            filePath);
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

                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = reader.ReadLine()) != null)
                {
                    string[] row = _dataService.SplitLineData(currentLine);
                    var listViewItem = new ListViewItem(row);
                    listViewDataList.Items.Add(listViewItem);
                }

                CalculateTotals();
                // auto-adjust the ID column width based on text
                listViewDataList.Columns[0].Width = -1;
            }
        }

        private void CalculateTotals()
        {
            decimal bedtimeTotal = 0;
            decimal waketimeTotal = 0;

            for (int i = 0; i < listViewDataList.Items.Count; i++)
            {
                bedtimeTotal += Convert.ToDecimal(listViewDataList.Items[i].SubItems[2].Text);
                waketimeTotal += Convert.ToDecimal(listViewDataList.Items[i].SubItems[4].Text);
            }

            BedtimeAvg = _dataService.TimeAverage(bedtimeTotal, listViewDataList.Items.Count).ToString();
            WaketimeAvg = _dataService.TimeAverage(waketimeTotal, listViewDataList.Items.Count).ToString();
        }

        private void ClearAllData()
        {
            listViewDataList.Items.Clear();
            ClearAverages();
            ClearTextData();
        }

        private void ClearAverages()
        {
            BedtimeAvg = "0";
            WaketimeAvg = "0";
        }

        private void ClearTextData()
        {
            IdInForm = "";
            BedtimeEnteredInForm = "";
            BedtimeDecInForm = "";
            WaketimeEnteredInForm = "";
            WaketimeDecInForm = "";
        }

        private Stream ShowOpenDialog()
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
                                 openFileDialog.InitialDirectory = "";
                                 openFileDialog.Filter = csvExt;
                                 openFileDialog.FilterIndex = 2;
                                 openFileDialog.RestoreDirectory = true;

            switch (openFileDialog.ShowDialog())
            {
                case DialogResult.OK:
                    return openFileDialog.OpenFile();
                default:
                    return null;
            }
        }

        private string ShowSaveDialog()
        {
            using SaveFileDialog exportFileDialog = new SaveFileDialog();
                                 exportFileDialog.Filter = csvExt;
                                 exportFileDialog.FilterIndex = 2;
                                 exportFileDialog.RestoreDirectory = true;

            Stream fileStream;
            switch (exportFileDialog.ShowDialog())
            {
                case DialogResult.OK when (fileStream = exportFileDialog.OpenFile()) != null:
                    fileStream.Close();
                    return exportFileDialog.FileName;
                default:
                    return null;
            }
        }

        private void DataList_DeleteItem(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                _listviewService.DeleteSelectedItems(sender);
                CalculateTotals();
            }
        }
    }
}
