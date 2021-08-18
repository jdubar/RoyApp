using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoyApp.interfaces;
using System;
using System.Windows.Forms;

namespace RoyApp.Tests
{
    [TestClass()]
    public class ListviewActionsTests
    {
        [TestMethod()]
        public void ListViewToCSV_ListViewIsNull_ShouldThrowException()
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
        public void ListViewToCSV_FilePathIsNull_ShouldThrowException()
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
        [TestMethod()]
        public void WriteCSVRow_ResultIsNull_ShouldThrowException()
        {
            StringBuilderService TestResult = null;
            int TestItemCount = 1;
            bool TestIncludeHidden = true;
            string TestString = "Testing";
            var expectedParamName = "result";

            try
            {
                ListviewActions.WriteCSVRow(TestResult, TestItemCount, i => TestIncludeHidden, i => TestString);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedParamName, ex.ParamName);
            }
        }
    }
}