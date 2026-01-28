using ATW.CommonBase.Model.Log;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ATW.CommonBase.File.Excel
{
    public static class Excel_Export_NPOI
    {

        #region 将实体集合导出为 Excel xlsx 文件。

        /// <summary>
        /// 将实体集合导出为 Excel xlsx 文件。
        /// 仅导出带有 EntityDataCheckModel 且 ColumnDescription 不为空的属性。
        /// 默认首列为 No 序号列（自增 1）。
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">实体集合</param>
        /// <param name="filePath">xlsx 文件保存路径</param>
        /// <param name="sheetName">工作表名称</param>
        public static void ExportEntitiesToXlsxFile<T>(IEnumerable<T> data, string filePath, string sheetName = "Sheet1")
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("filePath is required.", nameof(filePath));
            }

            var source = data ?? Enumerable.Empty<T>();
            var type = typeof(T);

            var columns = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(p => new
                {
                    Property = p,
                    Attribute = p.GetCustomAttribute<EntityDataCheckModel>(true)
                })
                .Where(x => x.Attribute != null && !string.IsNullOrWhiteSpace(x.Attribute.ColumnDescription))
                .OrderBy(x => x.Property.MetadataToken)
                .ToList();

            IWorkbook workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(string.IsNullOrWhiteSpace(sheetName) ? "Sheet1" : sheetName);

            var headerStyle = CreateBorderedStyle(workbook, isBold: true);
            var dataStyle = CreateBorderedStyle(workbook, isBold: false);

            var headerRow = sheet.CreateRow(0);
            var headerNoCell = headerRow.CreateCell(0);
            headerNoCell.SetCellValue("No");
            headerNoCell.CellStyle = headerStyle;
            for (var i = 0; i < columns.Count; i++)
            {
                var cell = headerRow.CreateCell(i + 1);
                cell.SetCellValue(columns[i].Attribute.ColumnDescription);
                cell.CellStyle = headerStyle;
            }

            var rowIndex = 1;
            foreach (var item in source)
            {
                var row = sheet.CreateRow(rowIndex);
                var noCell = row.CreateCell(0);
                noCell.SetCellValue(rowIndex);
                noCell.CellStyle = dataStyle;

                for (var colIndex = 0; colIndex < columns.Count; colIndex++)
                {
                    var cell = row.CreateCell(colIndex + 1);
                    var value = columns[colIndex].Property.GetValue(item);
                    SetCellValue(cell, value);
                    cell.CellStyle = dataStyle;
                }

                rowIndex++;
            }

            for (var i = 0; i < columns.Count + 1; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                workbook.Write(stream);
            }
        }

        private static void SetCellValue(ICell cell, object value)
        {
            if (value == null)
            {
                cell.SetCellValue(string.Empty);
                return;
            }

            switch (value)
            {
                case DateTime dt:
                    cell.SetCellValue(dt);
                    return;
                case bool b:
                    cell.SetCellValue(b);
                    return;
                case byte or sbyte or short or ushort or int or uint or long or ulong or float or double or decimal:
                    cell.SetCellValue(Convert.ToDouble(value));
                    return;
                default:
                    cell.SetCellValue(value.ToString());
                    return;
            }
        }

        private static ICellStyle CreateBorderedStyle(IWorkbook workbook, bool isBold)
        {
            var style = workbook.CreateCellStyle();
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            if (isBold)
            {
                var font = workbook.CreateFont();
                font.IsBold = true;
                style.SetFont(font);
            }

            return style;
        }

        #endregion

    }
}
