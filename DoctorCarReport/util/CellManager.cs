﻿using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCarReport.util
{
    class CellManager
    {
        public static void columnMerge(ISheet sheet, int row, int startColumn, int endColumn, bool center)
        {
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(row, row, startColumn, endColumn));

        }

        internal static ICell CreateCell(IWorkbook workbook, IRow row, int column, string content, bool isCentered)
        {
            ICellStyle style = workbook.CreateCellStyle();
            ICell cell = row.CreateCell(column);
            cell.SetCellValue(content);
            if (isCentered)
            {
                style.Alignment = HorizontalAlignment.Center;

            }
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;

            cell.CellStyle = style;
            return cell;
        }

        internal static ICell CreateCell(IWorkbook workbook, IRow row, int column, double content, bool isCentered)
        {
            ICellStyle style = workbook.CreateCellStyle();
            ICell cell = row.CreateCell(column);
            cell.SetCellValue(content);
            if (isCentered)
            {
                style.Alignment = HorizontalAlignment.Center;

            }
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;

            cell.CellStyle = style;
            return cell;
        }

        internal static void columnMerge(ISheet sheet, ICellStyle cellStyle, int row, int startColumn, int endColumn)
        {
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(row, row, startColumn, endColumn));
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
        }


        internal static void topBottomBorder(IWorkbook workbook, ICell cell, IRow row, int numCells)
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            for (int i = 1; i <= numCells; i++)
            {
                ICell newCell = row.CreateCell(cell.ColumnIndex + i);
                newCell.CellStyle = cellStyle;
            }
        }

        internal static ICell CreateLabelCell(IWorkbook workbook, IRow row, int column, string content, bool isCentered)
        {
            ICellStyle style = workbook.CreateCellStyle();
            ICell cell = row.CreateCell(column);
            cell.SetCellValue(content);
            style.Alignment = HorizontalAlignment.Right;
            cell.CellStyle = style;
            return cell;
        }

        internal static ICell CreateTotalCell(IWorkbook workbook, IRow row, int column, string content, bool isCentered)
        {
            ICellStyle style = workbook.CreateCellStyle();
            ICell cell = row.CreateCell(column);
            cell.SetCellValue(content);
            if (isCentered)
            {
                style.Alignment = HorizontalAlignment.Center;

            }
            style.BorderBottom = BorderStyle.Double;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;

            cell.CellStyle = style;
            return cell;
        }
    }
}
