namespace RoyApp.Tests
{
    public class ListviewActionsTests
    {
        //[TestMethod()]
        //public void ListViewToCSV_FilePathIsNull_ShouldThrowException()
        //{
        //    ListView TestList = new ListView();
        //    var expectedParamName = "filePath";
        //    string FilePath = null;
        //    bool IncludeHidden = true;

        //    try
        //    {
        //        FileService.ExportToCsv(TestList, FilePath, IncludeHidden);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Assert.AreEqual(expectedParamName, ex.ParamName);
        //    }
        //}
        //[TestMethod()]
        //public void WriteCSVRow_ResultIsNull_ShouldThrowException()
        //{
        //    IStringBuilderService TestResult = null;
        //    int TestItemCount = 1;
        //    bool TestIncludeHidden = true;
        //    string TestString = "Testing";
        //    var expectedParamName = "result";

        //    try
        //    {
        //        FileService.WriteCSVRow(TestResult, TestItemCount, i => TestIncludeHidden, i => TestString);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Assert.AreEqual(expectedParamName, ex.ParamName);
        //    }
        //}
        //[TestMethod()]
        //public void WriteCSVRow_WriteSingleString_StringValues_ShouldMatch()
        //{
        //    IStringBuilderService service = new StringService.StringBuilderService();
        //    int TestItemCount = 1;
        //    bool TestIncludeHidden = true;
        //    string TestString = "Testing";
        //    string expected = String.Format("\"{0}\"\r\n", TestString);


        //    FileService.WriteCSVRow(service, TestItemCount, i => TestIncludeHidden, i => TestString);
        //    Assert.AreEqual(expected, service.ToString());
        //}
        //[TestMethod()]
        //public void WriteCSVRow_WriteMultipleStrings_StringValues_ShouldMatch()
        //{
        //    IStringBuilderService service = new StringService.StringBuilderService();
        //    int TestItemCount = 3;
        //    bool TestIncludeHidden = true;
        //    string TestString = "Testing";
        //    string expected = string.Empty;
        //    for (int i = 0; i < TestItemCount; i++)
        //    {
        //        expected += String.Format("\"{0}\"", TestString);
        //        if (i < (TestItemCount - 1))
        //        {
        //            expected += ",";
        //        }
        //    }
        //    expected += "\r\n";

        //    FileService.WriteCSVRow(service, TestItemCount, i => TestIncludeHidden, i => TestString);
        //    Assert.AreEqual(expected, service.ToString());
        //}
    }
}