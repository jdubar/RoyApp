namespace RoyApp.Interfaces
{
    public interface IDataService
    {
        string[] SplitLineData(string currentLine);
        decimal TimeAverage(decimal timeTotal, int count);
        decimal TimeDuration(string bedtimeRec, string waketimeRec);
        decimal TimeToDecimal(string time);
    }
}
