using RoyApp.Gui.Components;
using RoyApp.Interfaces;
using RoyApp.Services;
using RoyApp.Wrappers;
using System;
using System.Windows.Forms;

namespace RoyApp
{
    public partial class MainForm : Form, IMainFormData
    {
        private readonly IDataService _dataService;
        private readonly IListviewService _listviewService;

        public MainForm(IDataService dataService, IListviewService listviewService)
        {
            InitializeComponent();
            _dataService = dataService;
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

        public void SetBedtimeAvg(string value) => bedtimeAvg.Text = value;

        public void SetWaketimeAvg(string value) => waketimeAvg.Text = value;

        private void BedTime_TextChanged(object sender, EventArgs e) => BedtimeDecInForm = _dataService.TimeToDecimal(BedtimeEnteredInForm).ToString();

        private void Waketime_TextChanged(object sender, EventArgs e) => WaketimeDecInForm = _dataService.TimeToDecimal(WaketimeEnteredInForm).ToString();

        private void Add_OnClick(object sender, EventArgs e)
        {
            var duration = _dataService.TimeDuration(BedtimeDecInForm, WaketimeDecInForm).ToString();
            if (duration == "-1")
            {
                var messageBox = new ViewMessageBox(new MessageBoxService());
                messageBox.ValueMissing();
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
            var messageBox = new ViewMessageBox(new MessageBoxService());
            var filePath = new ViewDialog(new DialogWrapper()).ShowSaveDialog();
            if (filePath != null)
            {
                try
                {
                    var fileService = new FileService(new FileIoWrapper(_dataService));
                    fileService.WriteDataToFile(filePath,
                    _listviewService.GetHeaderList(listViewDataList),
                    _listviewService.GetItemList(listViewDataList));
                    messageBox.ExportSuccess();
                }
                catch (Exception ex)
                {
                    messageBox.Error(ex);
                }
            }
        }

        private void Import_OnClick(object sender, EventArgs e)
        {
            var filePath = new ViewDialog(new DialogWrapper()).ShowOpenDialog();
            if (filePath != null)
            {
                ClearAllData();

                var fileService = new FileService(new FileIoWrapper(_dataService));
                var data = fileService.ReadImportData(filePath);
                _listviewService.AddListOfItemsToListView(listViewDataList, data);

                CalculateTotals();
                _listviewService.ColumnSetAutoAdjust(listViewDataList, 0);
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
