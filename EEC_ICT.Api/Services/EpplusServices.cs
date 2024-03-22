using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;

namespace EEC_ICT.Api.Services
{
    public class EpplusServices
    {
        public static double MeasureTextHeight(string text, ExcelFont font, double width)
        {
            if (string.IsNullOrEmpty(text)) return 0.0;
            var bitmap = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(bitmap);

            var pixelWidth = Convert.ToInt32(width * 7);  //7 pixels per excel column width
            var fontSize = font.Size * 1.01f;
            var drawingFont = new Font(font.Name, fontSize);
            var size = graphics.MeasureString(text, drawingFont, pixelWidth, new StringFormat { FormatFlags = StringFormatFlags.MeasureTrailingSpaces });

            //72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
            return Math.Min(Convert.ToDouble(size.Height) * 72 / 96, 409);
        }

        public static void SetCellExcel(ExcelRange range, ExcelHorizontalAlignment horizontalAlignment, ExcelVerticalAlignment verticalAlignment, bool merge, bool bold, Font font)
        {
            if (merge) range.Merge = true;
            range.Style.HorizontalAlignment = horizontalAlignment;
            range.Style.VerticalAlignment = verticalAlignment;
            range.Style.Font.SetFromFont(font);
            range.Style.Font.Size = font.Size;
            if (bold) range.Style.Font.Bold = true; else range.Style.Font.Bold = false;
        }

        public static void SetBorder(ExcelRange range, Color color, bool top, bool right, bool bottom, bool left)
        {
            if (top)
            {
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Top.Color.SetColor(color);
            }
            if (bottom)
            {
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Color.SetColor(color);
            }
            if (right)
            {
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Color.SetColor(color);
            }
            if (left)
            {
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Color.SetColor(color);
            }
        }
    }
}