using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Utility
{
    public static class NpoiHelper
    {
        /// <summary>
        /// 将datatable导出为excel
        /// 图片默认显示在excel 第二行最后一列
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="excelInfo">Tuple<excel列名,datatable列名,excel列宽度></param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="picBytes">导出图片字节流</param>
        /// <param name="mergedRegion">合并单元格信息：null不合并单元格</param>
        /// <returns></returns>
        public static MemoryStream ExportToExcel2007(DataTable table, List<Tuple<string, string, int>> excelInfo, string sheetName, byte[] picBytes, List<CellRangeAddress> mergedRegion)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                using (table)
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet(sheetName);
                    for (int i = 0; i < excelInfo.Count; i++)
                    {
                        sheet.SetColumnWidth(i, excelInfo[i].Item3 * 256);
                    }
                    IRow headerRow = sheet.CreateRow(0);
                    for (int i = 0; i < excelInfo.Count; i++)
                    {
                        headerRow.CreateCell(i).SetCellValue(excelInfo[i].Item1);
                    }
                    int rowIndex = 1;
                    foreach (DataRow row in table.Rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        for (int i = 0; i < excelInfo.Count; i++)
                        {
                            dataRow.CreateCell(i).SetCellValue(row[excelInfo[i].Item2].ToString());
                        }
                        rowIndex++;
                    }
                    //合并单元格
                    if (mergedRegion != null && mergedRegion.Count > 0)
                    {
                        foreach (CellRangeAddress cellRangeAddress in mergedRegion)
                        {
                            //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
                            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                            sheet.AddMergedRegion(cellRangeAddress);
                            ICellStyle style = workbook.CreateCellStyle();

                            //设置单元格的样式：水平对齐居中
                            style.Alignment = HorizontalAlignment.Center;
                            //将新的样式赋给单元格
                            var cell = sheet.GetRow(cellRangeAddress.FirstRow).GetCell(cellRangeAddress.FirstColumn);
                            cell.CellStyle = style;
                        }
                    }
                    //插入图片
                    if (picBytes != null && picBytes.Length > 0)
                    {
                        var row1 = 2;
                        var col1 = excelInfo.Count + 1;
                        /* Add Picture to Workbook, Specify picture type as PNG and Get an Index */
                        int pictureIdx = workbook.AddPicture(picBytes, PictureType.PNG);  //添加图片
                        /* Create the drawing container */
                        IDrawing drawing = (XSSFDrawing)sheet.CreateDrawingPatriarch();
                        /* Create an anchor point */
                        IClientAnchor anchor = new XSSFClientAnchor(1, 1, 0, 0, col1, row1, col1 + 1, row1 + 1);

                        /* Invoke createPicture and pass the anchor point and ID */
                        IPicture picture = (XSSFPicture)drawing.CreatePicture(anchor, pictureIdx);
                        /* Call resize method, which resizes the image */
                        picture.Resize();

                        picBytes = null;
                    }
                    workbook.Write(ms);
                    workbook.Close();
                }
            }
            catch (Exception ex)
            {
                ms = null;
            }
            return ms;
        }

        /// <summary>
        /// 在单元格中插入图片
        /// </summary>
        private static void InsertImage(ICell cell, byte[] picBytes, PictureType pt)
        {
            var row1 = cell.RowIndex;
            var col1 = cell.ColumnIndex;
            var workbook = cell.Sheet.Workbook;
            var sheet = cell.Sheet;

            /* Calcualte the image size */
            MemoryStream ms = new MemoryStream(picBytes);
            Image Img = Bitmap.FromStream(ms, true);
            double ImageOriginalWidth = Img.Width;
            double ImageOriginalHeight = Img.Height;
            double scale = cell.Sheet.GetColumnWidth(cell.ColumnIndex)/36.5 / ImageOriginalWidth;

            cell.Row.Height = (short)(ImageOriginalHeight * scale * 15);
            //cell.Sheet.SetColumnWidth(cell.ColumnIndex, (int)(ImageOriginalWidth * scale * 36.5));

            /* Add Picture to Workbook, Specify picture type as PNG and Get an Index */
            int pictureIdx = workbook.AddPicture(picBytes, pt);  //添加图片

            /* Create the drawing container */
            IDrawing drawing = sheet.CreateDrawingPatriarch();

            /* Create an anchor point */
            IClientAnchor anchor = drawing.CreateAnchor(0, 0, 0, 0, col1, row1, col1 + 1, row1 + 1);

            /* Invoke createPicture and pass the anchor point and ID */
            IPicture picture = drawing.CreatePicture(anchor, pictureIdx);
            /* Call resize method, which resizes the image */
            picture.Resize(1);

            picBytes = null;
        }

        private static void SetCellValue(this ICell cell, object value, ExcelTemplateColumn column)
        {
            if (Enum.TryParse(column.Format, out PictureType pt))
            {
                InsertImage(cell, (byte[])value, pt);
            }
            else if (Enum.TryParse(column.Format, out CellType type))
            {
                cell.SetCellType(type);
                cell.SetCellValue(value.ToString());
            }
            else
            {
                cell.SetCellValue(value.ToString());
            }
        }

        /// <summary>
        /// 导出对象列表
        /// </summary>
        public static void ExportList<T>(List<T> data, ISheet sheet, ExcelTemplate template)
        {
            var props = typeof(T).GetProperties();

            IRow headerRow = sheet.CreateRow(0);
            var columns = template.Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                var column = columns[i];
                cell.SetCellValue(column.ColumnTitle);
                if (column.Width > 0)
                {
                    cell.Sheet.SetColumnWidth(cell.ColumnIndex, columns[i].Width * 256);
                }
            }
            int rowIndex = 1;
            foreach (var row in data)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                for (int i = 0; i < columns.Count; i++)
                {
                    var pr = props.FirstOrDefault(p => p.Name == columns[i].ColumnName);
                    dataRow.CreateCell(i).SetCellValue(pr.GetValue(row), columns[i]);
                }
                rowIndex++;
            }

        }

        /// <summary>  
        /// 根据Excel列类型获取列的值  
        /// </summary>  
        /// <param name="cell">Excel列</param>  
        /// <returns></returns>  
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        IFormulaEvaluator e = cell.Sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

    }

    public class ExcelTemplate
    {
        public string TemplateID { get; set; }

        public string TemplateName { get; set; }

        public List<ExcelTemplateColumn> Columns { get; set; }

    }

    public class ExcelTemplateColumn
    {
        public string ColumnName { get; set; }

        public string ColumnTitle { get; set; }

        public short Width { get; set; }

        public short Height { get; set; }

        public string Format { get; set; }

    }


}
