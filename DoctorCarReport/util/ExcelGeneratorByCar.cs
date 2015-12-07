using DoctorCarReport.Service;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DoctorCarReport.util
{
    class ExcelGeneratorByCar
    {
        public void createExcelReport(string savePath, tbl_drive drive)
        {

        }

        internal void createExcelReport(string filePath, string carRegNo, string organizationName, List<DriveHistoryView> recordList)
        {
            try
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Drive Record");

                int rowIndex = 0;
                IRow row = sheet.CreateRow(rowIndex);

                ICell cell = row.CreateCell(0);
                IFont font = workbook.CreateFont();
                font.Boldweight = (short)FontBoldWeight.Bold;
                cell.SetCellValue("Doctor Car Drive Operation Management System Report - Drive Records");
                cell.CellStyle.SetFont(font);
                CellManager.columnMerge(sheet, cell.CellStyle, rowIndex, 0, 4);

                //cell = row.CreateCell(5);
                //cell.SetCellValue(record.StartDate.ToString());
                //CellManager.columnMerge(sheet, rowIndex, 5, 6, false);

                rowIndex++;
                sheet.CreateRow(rowIndex);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);

                cell = CellManager.CreateCell(workbook, row, 1, "Car Reg No", false);
                CellStyle.GrayBackground(workbook, cell, false);
                cell = CellManager.CreateCell(workbook, row, 2, carRegNo, false);
                CellManager.topBottomBorder(workbook, cell, row, 1);
                CellManager.columnMerge(sheet, rowIndex, 2, 3, true);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Organization", false);
                CellStyle.GrayBackground(workbook, cell, false);
                cell = CellManager.CreateCell(workbook, row, 2, organizationName.Trim(), false);
                CellManager.topBottomBorder(workbook, cell, row, 1);
                CellManager.columnMerge(sheet, rowIndex, 2, 3, true);

                rowIndex++;
                sheet.CreateRow(rowIndex);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = row.CreateCell(1);
                cell.SetCellValue("Drive Details");
                CellManager.topBottomBorder(workbook, cell, row, 11);
                CellManager.columnMerge(sheet, rowIndex, 1, 12, true);
                CellStyle.GrayBackground(workbook, cell, true);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "#", true);
                cell = CellManager.CreateCell(workbook, row, 2, "Start Date time", true);
                cell = CellManager.CreateCell(workbook, row, 3, "End Date Time", true);
                cell = CellManager.CreateCell(workbook, row, 4, "Operation Time (h)", true);
                cell = CellManager.CreateCell(workbook, row, 5, "Start Odometer (km)", true);
                cell = CellManager.CreateCell(workbook, row, 6, "End Odometer (km)", true);
                cell = CellManager.CreateCell(workbook, row, 7, "Distance (km)", true);
                cell = CellManager.CreateCell(workbook, row, 8, "Velocity (km/h)", true);
                cell = CellManager.CreateCell(workbook, row, 9, "Drive From", true);
                cell = CellManager.CreateCell(workbook, row, 10, "Drive To", true);
                cell = CellManager.CreateCell(workbook, row, 11, "Driver", true);
                cell = CellManager.CreateCell(workbook, row, 12, "No. Patients", true);

                int recordNo = 1;
                foreach (DriveHistoryView record in recordList)
                {

                    rowIndex++;
                    row = sheet.CreateRow(rowIndex);
                    cell = CellManager.CreateCell(workbook, row, 1, recordNo, true);
                    cell = CellManager.CreateCell(workbook, row, 2, record.StartDate.ToString(), false);
                    cell = CellManager.CreateCell(workbook, row, 3, record.EndDate.ToString(), false);
                    TimeSpan duration = ((DateTime)record.EndDate - (DateTime)record.StartDate);
                    cell = CellManager.CreateCell(workbook, row, 4, duration.Hours, false);
                    cell = CellManager.CreateCell(workbook, row, 5, record.StartOdometer, false);
                    cell = CellManager.CreateCell(workbook, row, 6, record.EndOdometer, false);
                    cell = CellManager.CreateCell(workbook, row, 7, (record.EndOdometer - record.StartOdometer), false);
                    if (duration.Hours > 0)
                    {
                        cell = CellManager.CreateCell(workbook, row, 8, Math.Round((double)(record.EndOdometer - record.StartOdometer) / duration.Hours, 1), false);
                    }
                    else
                    {
                        cell = CellManager.CreateCell(workbook, row, 8, "", false);
                    }
                    cell = CellManager.CreateCell(workbook, row, 9, record.From.ToString(), false);
                    cell = CellManager.CreateCell(workbook, row, 10, record.To.ToString(), false);
                    cell = CellManager.CreateCell(workbook, row, 11, record.DriverName.ToString(), false);
                    cell = CellManager.CreateCell(workbook, row, 12, record.NumPatients, false);

                    recordNo++;

                    for (int i = 0; i < 15; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                }

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                if (recordNo - 1 > 0)
                {
                    cell = CellManager.CreateTotalCell(workbook, row, 12, "", false);
                    cell.SetCellFormula("SUM(M" + (rowIndex + 1 - recordNo + 1) + ":M" + rowIndex + ")");
                }

                sheet = workbook.CreateSheet("Fuel Record");
                rowIndex = 0;
                row = sheet.CreateRow(rowIndex);
                cell = row.CreateCell(0);
                cell.SetCellValue("Doctor Car Drive Operation Management System Report - Fuel Records");
                CellManager.columnMerge(sheet, cell.CellStyle, rowIndex, 0, 4);

                rowIndex++;
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Fuel Expenses", true);
                CellManager.topBottomBorder(workbook, cell, row, 3);
                CellStyle.GrayBackground(workbook, cell, true);
                CellManager.columnMerge(sheet, rowIndex, 1, 4, true);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Date Time", true);
                cell = CellManager.CreateCell(workbook, row, 2, "Odometer (km)", true);
                cell = CellManager.CreateCell(workbook, row, 3, "Capacity (L)", true);
                cell = CellManager.CreateCell(workbook, row, 4, "Amount(SDG)", true);

                int numFuelRecords = 0;
                foreach (DriveHistoryView record in recordList)
                {

                    if (record.FuelTopup != null)
                    {
                        foreach (tbl_fuel fuel in record.FuelTopup)
                        {
                            rowIndex++;
                            row = sheet.CreateRow(rowIndex);
                            cell = CellManager.CreateCell(workbook, row, 1, fuel.ADD_TIME.ToString(), false);
                            cell = CellManager.CreateCell(workbook, row, 2, fuel.ODOMETER, false);
                            cell = CellManager.CreateCell(workbook, row, 3, fuel.VOLUME, false);
                            cell = CellManager.CreateCell(workbook, row, 4, fuel.AMOUNT, false);
                            numFuelRecords++;
                        }
                    }
                }
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                if (numFuelRecords > 0)
                {
                    cell = CellManager.CreateLabelCell(workbook, row, 2, "Total", false);
                    cell = CellManager.CreateTotalCell(workbook, row, 3, "", false);
                    cell.SetCellFormula("SUM(D" + (rowIndex + 1 - numFuelRecords) + ":D" + rowIndex + ")");
                    cell = CellManager.CreateTotalCell(workbook, row, 4, "", false);
                    cell.SetCellFormula("SUM(E" + (rowIndex + 1 - numFuelRecords) + ":E" + rowIndex + ")");
                }




                for (int i = 0; i < 15; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                sheet = workbook.CreateSheet("Other Expenses Record");
                rowIndex = 0;
                row = sheet.CreateRow(rowIndex);
                cell = row.CreateCell(0);
                cell.SetCellValue("Doctor Car Drive Operation Management System Report - Other Expenses");
                CellManager.columnMerge(sheet, cell.CellStyle, rowIndex, 0, 5);

                rowIndex++;
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Other Expenses", true);
                CellManager.topBottomBorder(workbook, cell, row, 3);
                CellStyle.GrayBackground(workbook, cell, true);
                CellManager.columnMerge(sheet, rowIndex, 1, 4, true);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Date Time", true);
                cell = CellManager.CreateCell(workbook, row, 2, "Item", true);
                cell = CellManager.CreateCell(workbook, row, 3, "Amount(SDG)", true);
                cell = CellManager.CreateCell(workbook, row, 4, "Description", true);

                DrCarDriveService service = new DrCarDriveService();
                int numExpenses = 0;
                foreach (DriveHistoryView record in recordList)
                {
                    List<ExpenseView> expenses = service.getOtherExpensesByDriveId(record.DriveID);
                    if (expenses != null)
                    {
                        foreach (ExpenseView expense in expenses)
                        {
                            rowIndex++;
                            row = sheet.CreateRow(rowIndex);
                            cell = CellManager.CreateCell(workbook, row, 1, expense.AddDate.ToString(), false);
                            cell = CellManager.CreateCell(workbook, row, 2, expense.Item, false);
                            cell = CellManager.CreateCell(workbook, row, 3, expense.Amount, false);
                            cell = CellManager.CreateCell(workbook, row, 4, expense.Remark, false);
                            numExpenses++;
                        }
                    }
                }
                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                if (numExpenses > 0)
                {
                    cell = CellManager.CreateLabelCell(workbook, row, 2, "Total", false);
                    cell = CellManager.CreateTotalCell(workbook, row, 3, "", false);
                    cell.SetCellFormula("SUM(D" + (rowIndex + 1 - numExpenses) + ":D" + rowIndex + ")");
                }


                for (int i = 0; i < 15; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                FileStream stream = File.Create(filePath);
                workbook.Write(stream);
                stream.Close();
            }
            catch (Exception ex)
            {
            }

        }
    }
}
