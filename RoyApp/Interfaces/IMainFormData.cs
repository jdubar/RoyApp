namespace RoyApp.Interfaces
{
    public interface IMainFormData
    {
        string IdInForm { get; set; }
        string BedtimeEnteredInForm { get; set; }
        string WaketimeEnteredInForm { get; set; }
        string BedtimeDecInForm { get; set; }
        string WaketimeDecInForm { get; set; }

        void SetBedtimeAvg(string value);

        void SetWaketimeAvg(string value);
    }
}