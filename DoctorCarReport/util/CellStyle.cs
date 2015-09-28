using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCarReport.util
{
    class CellStyle
    {
        public static void HorizontalCenter(ICellStyle style, ICell cell)
        {
            style.Alignment = HorizontalAlignment.Center;
            cell.CellStyle = style;
        }

        internal static void GrayBackground(IWorkbook workbook, ICell cell, bool isCentered)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            if (isCentered)
            {
                style.Alignment = HorizontalAlignment.Center;
            }
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            cell.CellStyle = style;
        }
    }
}
