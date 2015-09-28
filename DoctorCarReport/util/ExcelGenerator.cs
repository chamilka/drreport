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
    class ExcelGenerator
    {
        public void createExcelReport(string savePath, tbl_drive drive)
        {

        }

        internal void createExcelReport(string filePath, string carRegNo, List<DriveHistoryView> recordList)
        {
            IWorkbook workbook = new XSSFWorkbook();

            foreach (DriveHistoryView record in recordList)
            {
                ISheet sheet = workbook.CreateSheet(record.StartDate.Value.ToString("yyyy-MM-dd") +"_"+ new Random().Next(100).ToString());

                int rowIndex = 0;
                IRow row = sheet.CreateRow(rowIndex);

                ICell cell = row.CreateCell(0);
                cell.SetCellValue("Doctor Car Drive Operation Management System Report");
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

                cell = CellManager.CreateCell(workbook, row, 4, "Start Odometer (km)", false);
                CellStyle.GrayBackground(workbook, cell, false);
                cell = CellManager.CreateCell(workbook, row, 5, Convert.ToDouble(record.StartOdometer), false);

                cell = CellManager.CreateCell(workbook, row, 7, "Record Date", false);
                CellStyle.GrayBackground(workbook, cell, false);
                cell = CellManager.CreateCell(workbook, row, 8, record.InsertDateTime.ToString(), false);


                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Operation Date", false);
                CellStyle.GrayBackground(workbook, cell, false);
                cell = CellManager.CreateCell(workbook, row, 2, record.StartDate.ToString(), false);
                
                cell = CellManager.CreateCell(workbook, row, 4, "End Odometer (km)", false);
                CellStyle.GrayBackground(workbook, cell, false);
                cell = CellManager.CreateCell(workbook, row, 5, Convert.ToDouble(record.EndOdometer), false);


                rowIndex++;
                row = sheet.CreateRow(rowIndex);

                cell = CellManager.CreateCell(workbook, row, 1, "Driver", false);
                CellStyle.GrayBackground(workbook, cell, false);
                cell = CellManager.CreateCell(workbook, row, 2, record.DriverName, false);

                cell = CellManager.CreateCell(workbook, row, 4, "Distance (km)", false);
                CellStyle.GrayBackground(workbook, cell, false);
                string distance = FindDifference.calculateDistance(record.StartOdometer, record.EndOdometer);
                try
                {
                    double dist = Convert.ToDouble(distance);
                    cell = CellManager.CreateCell(workbook, row, 5, dist, false);
                }
                catch
                {
                    cell = CellManager.CreateCell(workbook, row, 5, distance, false);
                }

                rowIndex++;
                sheet.CreateRow(rowIndex);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = row.CreateCell(1);
                cell.SetCellValue("Visit Details");
                CellManager.topBottomBorder(workbook, cell, row, 10);
                CellManager.columnMerge(sheet, rowIndex, 1, 11, true);
                CellStyle.GrayBackground(workbook, cell, true);

                cell = row.CreateCell(12);
                cell.SetCellValue("Medicine");
                CellManager.topBottomBorder(workbook, cell, row, 2);
                CellManager.columnMerge(sheet, rowIndex, 12, 14, true);
                CellStyle.GrayBackground(workbook, cell, true);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Place", true);
                cell = CellManager.CreateCell(workbook, row, 2, "Arrival Date time", true);
                cell = CellManager.CreateCell(workbook, row, 3, "Departure Date Time", true);
                cell = CellManager.CreateCell(workbook, row, 4, "Odometer (km)", true);
                cell = CellManager.CreateCell(workbook, row, 5, "No. Patients", true);
                cell = CellManager.CreateCell(workbook, row, 6, "No. Doctors", true);
                cell = CellManager.CreateCell(workbook, row, 7, "No. Nurses", true);
                cell = CellManager.CreateCell(workbook, row, 8, "Activity", true);
                CellManager.topBottomBorder(workbook, cell, row, 3);
                CellManager.columnMerge(sheet, rowIndex, 8, 11, true);

                cell = CellManager.CreateCell(workbook, row, 12, "Item", true);
                cell = CellManager.CreateCell(workbook, row, 13, "Dosage", true);
                cell = CellManager.CreateCell(workbook, row, 14, "Unit", true);

                DrCarDriveService service = new DrCarDriveService();
                if (record.Places != null)
                {
                    int numPlaces=0;
                    foreach (tbl_places place in record.Places)
                    {
                        rowIndex++;
                        row = sheet.CreateRow(rowIndex);
                        cell = CellManager.CreateCell(workbook, row, 1, place.VILLAGE_NAME, false);
                        cell = CellManager.CreateCell(workbook, row, 2, place.ARRIVAL_DATE.ToString(), false);
                        cell = CellManager.CreateCell(workbook, row, 3, place.DEPARTURE_DATE.ToString(), false);
                        cell = CellManager.CreateCell(workbook, row, 4, Convert.ToDouble(place.ARRIVAL_ODOMETER.ToString()), false);
                        cell = CellManager.CreateCell(workbook, row, 5, Convert.ToDouble(place.NUMBER_PATIENT.ToString()), false);
                        cell = CellManager.CreateCell(workbook, row, 6, Convert.ToDouble(place.NUMBER_DOCTORS.ToString()), false);
                        cell = CellManager.CreateCell(workbook, row, 7, Convert.ToDouble(place.NUMBER_PARAMEDICS.ToString()), false);
                        cell = CellManager.CreateCell(workbook, row, 8, place.REMARK, false);
                        CellManager.topBottomBorder(workbook, cell, row, 3);
                        CellManager.columnMerge(sheet, rowIndex, 8, 11, false);

                        List<tbl_issue_medicine> medicineList = service.getIssueMedicineByPlaceId(place.ID);
                        if (medicineList != null)
                        {
                            foreach (tbl_issue_medicine medicine in medicineList)
                            {
                                tbl_drive_medicine med = service.getMedicineById(medicine.FKY_MEDICINE);
                                cell = CellManager.CreateCell(workbook, row, 12, med.NAME, false);
                                cell = CellManager.CreateCell(workbook, row, 13, Convert.ToDouble(medicine.DOSAGE), false);
                                cell = CellManager.CreateCell(workbook, row, 14, medicine.UNIT, false);
                                rowIndex++;
                                row = sheet.CreateRow(rowIndex);
                            }
                        }
                        numPlaces++;
                    }
                    rowIndex++;
                    row = sheet.CreateRow(rowIndex);
                    if (numPlaces > 0)
                    {
                        cell = CellManager.CreateLabelCell(workbook, row, 4, "Total", false);
                        cell = CellManager.CreateTotalCell(workbook, row, 5, "", false);
                        //cell.SetCellFormula("SUM(F" + (rowIndex + 1 - numPlaces) + ":F" + rowIndex + ")");
                        cell.SetCellFormula("SUM(F" + "9" + ":F" + rowIndex + ")");
                    }
                                

                }

                rowIndex++;
                row = sheet.CreateRow(rowIndex);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Fuel Expenses", true);
                CellManager.topBottomBorder(workbook, cell, row, 2);
                CellStyle.GrayBackground(workbook, cell, true);
                CellManager.columnMerge(sheet, rowIndex, 1, 3, true);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Odometer", true);
                cell = CellManager.CreateCell(workbook, row, 2, "Capacity (L)", true);
                cell = CellManager.CreateCell(workbook, row, 3, "Amount(SDG)", true);

                if (record.FuelTopup != null)
                {
                    int numFuelRecords = 0;
                    foreach (tbl_fuel fuel in record.FuelTopup)
                    {
                        rowIndex++;
                        row = sheet.CreateRow(rowIndex);
                        cell = CellManager.CreateCell(workbook, row, 1, Convert.ToDouble(fuel.ODOMETER), false);
                        cell = CellManager.CreateCell(workbook, row, 2, Convert.ToDouble(fuel.VOLUME), false);
                        cell = CellManager.CreateCell(workbook, row, 3, Convert.ToDouble(fuel.AMOUNT), false);
                        numFuelRecords++;
                    }

                    rowIndex++;
                    row = sheet.CreateRow(rowIndex);
                    if (numFuelRecords > 0)
                    {
                        cell = CellManager.CreateLabelCell(workbook, row, 1, "Total", false);
                        cell = CellManager.CreateTotalCell(workbook, row, 2, "", false);
                        cell.SetCellFormula("SUM(C" + (rowIndex + 1 - numFuelRecords) + ":C" + rowIndex + ")");
                    }
                }


                rowIndex++;
                row = sheet.CreateRow(rowIndex);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Other Expenses", true);
                CellManager.topBottomBorder(workbook, cell, row, 2);
                CellStyle.GrayBackground(workbook, cell, true);
                CellManager.columnMerge(sheet, rowIndex, 1, 3, true);

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                cell = CellManager.CreateCell(workbook, row, 1, "Item", true);
                cell = CellManager.CreateCell(workbook, row, 2, "Amount(SDG)", true);
                cell = CellManager.CreateCell(workbook, row, 3, "Description", true);

                List<ExpenseView> expenses = service.getOtherExpensesByDriveId(record.DriveID);
                if (expenses != null)
                {
                    int numExpenses = 0;
                    foreach (ExpenseView expense in expenses)
                    {
                        rowIndex++;
                        row = sheet.CreateRow(rowIndex);
                        cell = CellManager.CreateCell(workbook, row, 1, expense.Item, false);
                        cell = CellManager.CreateCell(workbook, row, 2, Convert.ToDouble(expense.Amount), false);
                        cell = CellManager.CreateCell(workbook, row, 3, expense.Remark, false);
                        numExpenses++;
                    }

                    rowIndex++;
                    row = sheet.CreateRow(rowIndex);
                    if (numExpenses > 0)
                    {
                        cell = CellManager.CreateLabelCell(workbook, row, 1, "Total", false);
                        cell = CellManager.CreateTotalCell(workbook, row, 2, "", false);
                        cell.SetCellFormula("SUM(C" + (rowIndex + 1 - numExpenses) + ":C" + rowIndex + ")");
                    }
                    
                }

                for (int i = 0; i < 15; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }

            FileStream stream = File.Create(filePath);
            workbook.Write(stream);
            stream.Close();

        }
    }
}
