using RoyApp.Interfaces;
using RoyApp.Services;
using System;
using System.IO;
using System.Windows.Forms;

namespace RoyApp
{
    public partial class MainForm : Form, IMainFormData
    {
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;
        private readonly IListviewService _listviewService;
        private readonly IMessageBoxService _messageBoxService;
        private const string csvExt = "csv files (*.csv)|*.csv";

        public MainForm(IDataService dataService, IFileService fileService, IListviewService listviewService, IMessageBoxService messageBoxService)
        {
            InitializeComponent();
            _dataService = dataService;
            _fileService = fileService;
            _listviewService = listviewService;
            _messageBoxService = messageBoxService;
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

        public void SetBedtimeAvg(string value) => bedtimeAvg.Text = value;

        public void SetWaketimeAvg(string value) => waketimeAvg.Text = value;

        private void BedTime_TextChanged(object sender, EventArgs e) => BedtimeDecInForm = _dataService.TimeToDecimal(BedtimeEnteredInForm).ToString();

        private void Waketime_TextChanged(object sender, EventArgs e) => WaketimeDecInForm = _dataService.TimeToDecimal(WaketimeEnteredInForm).ToString();

        private void Add_OnClick(object sender, EventArgs e)
        {
            var duration = _dataService.TimeDuration(BedtimeDecInForm, WaketimeDecInForm).ToString();
            if (duration == "-1")
            {
                _messageBoxService.ValueMissing();
                return;
            }

            _listviewService.AddItemToListView(listViewDataList,
                                               new string[] { IdInForm, BedtimeEnteredInForm, BedtimeDecInForm, WaketimeEnteredInForm, WaketimeDecInForm, duration });

            CalculateTotals();
            _listviewService.ColumnSetAutoAdjust(listViewDataList, 0);
            ClearTextData();
        }

        private void Clear_OnClick(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void Export_OnClick(object sender, EventArgs e)
        {
            var filePath = ShowSaveDialog();
            if (filePath != null)
            {
                try
                {
                    _fileService.WriteToCsv(_listviewService.GetItemList(listViewDataList),
                                            _listviewService.GetHeaderList(listViewDataList),
                                            filePath);
                    _messageBoxService.ExportSuccess();
                }
                catch (Exception ex)
                {
                    _messageBoxService.Error(ex);
                }
            }
        }

        private void Import_OnClick(object sender, EventArgs e)
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

            SetBedtimeAvg(_dataService.TimeAverage(bedtimeTotal, listViewDataList.Items.Count).ToString());
            SetWaketimeAvg(_dataService.TimeAverage(waketimeTotal, listViewDataList.Items.Count).ToString());
        }

        private void ClearAllData()
        {
            listViewDataList.Items.Clear();
            ClearAverages();
            ClearTextData();
        }

        private void ClearAverages()
        {
            SetBedtimeAvg("0");
            SetWaketimeAvg("0");
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

            return openFileDialog.ShowDialog() switch
            {
                DialogResult.OK => openFileDialog.OpenFile(),
                _ => null,
            };
        }

        private string ShowSaveDialog()
        {
            using SaveFileDialog exportFileDialog = new SaveFileDialog();
            exportFileDialog.Filter = csvExt;
            exportFileDialog.FilterIndex = 2;
            exportFileDialog.RestoreDirectory = true;

            Stream fileStream;
            using var stream = fileStream = exportFileDialog.OpenFile();
            switch (exportFileDialog.ShowDialog())
            {
                case DialogResult.OK when stream != null:
                    fileStream.Close();
                    return exportFileDialog.FileName;
                default:
                    return null;
            }
        }

        private void DataList_DeleteItem(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                _listviewService.DeleteSelectedItems(sender);
                CalculateTotals();
            }
        }
    }
}
