using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelExample
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class ExcelClass
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Excel.Application excelApp = new Excel.Application();  // Creates a new Excel Application
			excelApp.Visible = true;  // Makes Excel visible to the user.

			// The following line if uncommented adds a new workbook
			//Excel.Workbook newWorkbook = excelApp.Workbooks.Add();
			
			// The following code opens an existing workbook
			string workbookPath = "c:/SomeWorkBook.xlsx";  // Add your own path here

            Excel.Workbook excelWorkbook = null;

            try
            {
                excelWorkbook = excelApp.Workbooks.Open(workbookPath, 0,
                    false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true,
                    false, 0, true, false, false);
            }
            catch
            {
                //Create a new workbook if the existing workbook failed to open.
                excelWorkbook = excelApp.Workbooks.Add();
            }
			
			// The following gets the Worksheets collection
			Excel.Sheets excelSheets = excelWorkbook.Worksheets;

			// The following gets Sheet1 for editing
			string currentSheet = "Sheet1";
			Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);
			
			// The following gets cell A1 for editing
			Excel.Range excelCell = (Excel.Range)excelWorksheet.get_Range("A1", "A1");

			// The following sets cell A1's value to "Hi There"
			excelCell.Value2 = "Hi There";
		}
	}
}
