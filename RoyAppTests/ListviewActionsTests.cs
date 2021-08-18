using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace RoyApp.Tests
{
    [TestClass()]
    public class ListviewActionsTests
    {
        [TestMethod()]
        public void ListViewToCSVTest_ListViewIsNull_ShouldThrowException()
        {
            ListView TestList = null;
            var expectedParamName = "listView";
            string FilePath = "C:\test.csv";
            bool IncludeHidden = true;

            try
            {
                ListviewActions.ListViewToCSV(TestList, FilePath, IncludeHidden);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedParamName, ex.ParamName);
            }
        }
        [TestMethod()]
        public void ListViewToCSVTest_FilePathIsNull_ShouldThrowException()
        {
            ListView TestList = new ListView();
            var expectedParamName = "filePath";
            string FilePath = null;
            bool IncludeHidden = true;

            try
            {
                ListviewActions.ListViewToCSV(TestList, FilePath, IncludeHidden);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedParamName, ex.ParamName);
            }
        }
    }
}