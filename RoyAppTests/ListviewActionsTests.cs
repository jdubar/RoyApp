using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        [TestMethod()]
        public void WriteCSVRow_WriteSingleString_StringValues_ShouldMatch()
        {
            StringBuilderService service = new StringBuilderService();
            int TestItemCount = 1;
            bool TestIncludeHidden = true;
            string TestString = "Testing";
            string expected = String.Format("\"{0}\"\r\n", TestString);


            ListviewActions.WriteCSVRow(service, TestItemCount, i => TestIncludeHidden, i => TestString);
            Assert.AreEqual(expected, service.ToString());
        }
        [TestMethod()]
        public void WriteCSVRow_WriteMultipleStrings_StringValues_ShouldMatch()
        {
            StringBuilderService service = new StringBuilderService();
            int TestItemCount = 3;
            bool TestIncludeHidden = true;
            string TestString = "Testing";
            string expected = string.Empty;
            for (int i = 0; i < TestItemCount; i++)
            {
                expected += String.Format("\"{0}\"", TestString);
                if (i < (TestItemCount - 1))
                {
                    expected += ",";
                }
            }
            expected += "\r\n";

            ListviewActions.WriteCSVRow(service, TestItemCount, i => TestIncludeHidden, i => TestString);
            Assert.AreEqual(expected, service.ToString());
        }
    }
}