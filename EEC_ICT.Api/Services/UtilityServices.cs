using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace EEC_ICT.Api.Services
{
    public class UtilityServices
    {
        public static string RandomShortString()
        {
            int length = 7;

            StringBuilder str_build = new StringBuilder();
            Random random = new Random();
            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            return str_build.ToString();
        }

        public static string GenerateSlug(string phrase)
        {
            string str = phrase.ToUpper();
            // invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens
            return str;
        }

        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }

        internal static object convertToUnSign(object tenLoaiNenNha)
        {
            throw new NotImplementedException();
        }

        public static string formatingNumber(int number)
        {
            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            nfi.CurrencyDecimalSeparator = ",";
            nfi.CurrencyGroupSeparator = ".";
            nfi.CurrencySymbol = "";
            nfi.CurrencyDecimalDigits = 0;
            var answer = Convert.ToDecimal(number).ToString("C",
                  nfi);
            return answer;
        }
        public static bool ExportWorkbookToPdf(string workbookPath, string outputPath)
        {
            // If either required string is null or empty, stop and bail out
            if (string.IsNullOrEmpty(workbookPath) || string.IsNullOrEmpty(outputPath))
            {
                return false;
            }

            // Create COM Objects
            Microsoft.Office.Interop.Excel.Application excelApplication;
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;

            // Create new instance of Excel
            excelApplication = new Microsoft.Office.Interop.Excel.Application();

            // Make the process invisible to the user
            excelApplication.ScreenUpdating = false;

            // Make the process silent
            excelApplication.DisplayAlerts = false;

            // Open the workbook that you wish to export to PDF
            excelWorkbook = excelApplication.Workbooks.Open(workbookPath);

            // If the workbook failed to open, stop, clean up, and bail out
            if (excelWorkbook == null)
            {
                excelApplication.Quit();

                excelApplication = null;
                excelWorkbook = null;

                return false;
            }

            var exportSuccessful = true;
            try
            {
                // Call Excel's native export function (valid in Office 2007 and Office 2010, AFAIK)
                excelWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, outputPath);
            }
            catch (System.Exception ex)
            {
                // Mark the export as failed for the return value...
                exportSuccessful = false;

                // Do something with any exceptions here, if you wish...
                // MessageBox.Show...        
            }
            finally
            {
                // Close the workbook, quit the Excel, and clean up regardless of the results...
                excelWorkbook.Close();
                excelApplication.Quit();

                excelApplication = null;
                excelWorkbook = null;
            }

            // You can use the following method to automatically open the PDF after export if you wish
            // Make sure that the file actually exists first...
            if (System.IO.File.Exists(outputPath))
            {
                System.Diagnostics.Process.Start(outputPath);
            }

            return exportSuccessful;
        }

        public static string CalculateThuTuDonVi (List<string> listMa, string ma)
        {
            if (listMa.Count == 0 || !listMa.Contains(ma))
            {
                return "";
            }

            listMa.Sort();

            List<string> listMaDonViDauMoi = listMa.Where(x => x.Length == 2).ToList();

            for (int i = 0; i < listMaDonViDauMoi.Count; i++)
            {
                if (listMaDonViDauMoi[i] == ma) return (i + 1).ToString();
                List<string> listDonViCap2ThuocDonVi = listMa.Where(x => x.Length == 4 && x.StartsWith(listMaDonViDauMoi[i])).ToList();

                for (int j = 0; j < listDonViCap2ThuocDonVi.Count; j++)
                {
                    if (listDonViCap2ThuocDonVi[j] == ma) return (i + 1).ToString() + "." +  (j + 1).ToString();

                    List<string> listDonViCap3ThuocDonVi = listMa.Where(x => x.Length == 6 && x.StartsWith(listDonViCap2ThuocDonVi[j])).ToList();

                    for (int k = 0; k < listDonViCap3ThuocDonVi.Count; k++)
                    {
                        if (listDonViCap3ThuocDonVi[k] == ma) return (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString();
                    }
                }
            }

            return "";
        }

        public static List<ThuTuAndMa> generateThuTuFromListMaDonVi (List<string> listMa)
        {
            List<ThuTuAndMa> result = new List<ThuTuAndMa> { };

            List<string> listMaDonViDauMoi = listMa.Where(x => x.Length == 2).ToList();
            
            for (int i = 0; i < listMaDonViDauMoi.Count; i++)
            {
                result.Add(new ThuTuAndMa { Ma = listMaDonViDauMoi[i], ThuTu = (i + 1).ToString() });
                List<string> listDonViCap2ThuocDonVi = listMa.Where(x => x.Length == 4 && x.StartsWith(listMaDonViDauMoi[i])).ToList();
                for (int j = 0; j < listDonViCap2ThuocDonVi.Count; j++)
                {
                    result.Add(new ThuTuAndMa { Ma = listDonViCap2ThuocDonVi[j], ThuTu = (i + 1).ToString() + "." + (j + 1).ToString() });

                    List<string> listDonViCap3ThuocDonVi = listMa.Where(x => x.Length == 6 && x.StartsWith(listDonViCap2ThuocDonVi[j])).ToList();

                    for (int k = 0; k < listDonViCap3ThuocDonVi.Count; k++)
                    {
                        result.Add(new ThuTuAndMa { Ma = listDonViCap3ThuocDonVi[k], ThuTu = (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() });
                    }
                }
            }



            return result;
        }



        //public static string CalculateThuTuVatTu(List<KiemKe_MaVatTu> listMa, string ma)
        //{
            
        //    if (listMa.Count == 0 || listMa.Where(x => x.MaVatTu == ma).Count() == 0)
        //    {
        //        return "";
        //    }

        //    //listMa.Sort();

        //    var listCap1 = listMa.Where(x => x.MaVatTu.EndsWith(".00.00.000")).ToList();

        //    for (int i = 0; i < listCap1.Count; i++)
        //    {
        //        if (listCap1[i].MaVatTu == ma) return (i + 1).ToString();
                

        //        var listCap2 = listMa.Where(x => x.MaCha == listCap1[i].MaVatTu).ToList();

        //        for (int j = 0; j < listCap2.Count; j++)
        //        {
        //            if (listCap2[j].MaVatTu == ma) return (i + 1).ToString() + "." + (j + 1).ToString();

        //            var listCap3 = listMa.Where(x => x.MaCha == listCap2[j].MaVatTu).ToList();

        //            for (int k = 0; k < listCap3.Count; k++)
        //            {
        //                if (listCap3[k].MaVatTu == ma) return (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString();

        //               var listCap4 = listMa.Where(x => x.MaCha == listCap3[k].MaVatTu).ToList();

        //                for (int t = 0; t < listCap4.Count; t++)
        //                {
        //                    if (listCap4[t].MaVatTu == ma) return (i + 1).ToString() + "." + (j + 1).ToString() + "." + (k + 1).ToString() + "." + (t + 1).ToString();
        //                }

        //            }
        //        }
        //    }

        //    return "";
        //}

        public static string MaVatTuRemoveZeros(string mavattu)
        {
            string endTrim = "";
            if (mavattu.Substring(mavattu.Length - 3, 3).EndsWith("000"))
            {
                endTrim = mavattu;
            } else
            {
                endTrim = mavattu.Substring(0, 6) + mavattu.Substring(6, mavattu.Length - 9).Replace(".00", "") + mavattu.Substring(mavattu.Length - 3, 3);
            }
           

            var result = "";
            if (mavattu.EndsWith(".00.00.000"))
            {
                result = endTrim.Substring(0, endTrim.Length - ".00.00.000".Length);
            }
            else if (endTrim.EndsWith(".00.000"))
            {
                result = endTrim.Substring(0, endTrim.Length - ".00.000".Length);
            }
            else if (mavattu.EndsWith(".000"))
            {
                result = endTrim.Substring(0, endTrim.Length - ".000".Length);
            }
            else
            {
                result = endTrim;
            }

            return result;
        }

        public static string MaVatTu_RemoveZeros(string mavattu)
        {
            var maVatTuSplit = mavattu.Split('.');
            for(int i=maVatTuSplit.Length-1; i>=0; i--)
            {
                if(maVatTuSplit[i] != "00" && maVatTuSplit[i] != "000")
                {
                    var ma = string.Join(".", maVatTuSplit.Take(i+1));
                    return string.Join(".", maVatTuSplit.Take(i+1));
                }
            }
            return "";
        }

        public static string MaVatTu_FindParent(string mavattu)
        {
            var maWoTail_Split = MaVatTu_RemoveZeros(mavattu).Split('.');
            var ma = MaVatTu_RemoveZeros(string.Join(".", maWoTail_Split.Take(maWoTail_Split.Length - 1)));
            return ma;
        }

        //public static List<string> GetAllChildrenWithMaVatTu (List<DM_TrangBiVatTu> listVatTu, string mavattu)
        //{
        //    List<string> result = new List<string> { };
        //    var listCap1 = listVatTu.Where(x => x.MaCha == mavattu).ToList();
        //    result.AddRange(listCap1.Select(x => x.MaVatTu));


        //    foreach (var vtLevel1 in listCap1)
        //    {
        //        var listCap2 = listVatTu.Where(x => x.MaCha == vtLevel1.MaVatTu).ToList();
        //        result.AddRange(listCap2.Select(x => x.MaVatTu));

        //        foreach (var vtLevel2 in listCap2)
        //        {
        //            var listCap3 = listVatTu.Where(x => x.MaCha == vtLevel2.MaVatTu).ToList();
        //            result.AddRange(listCap3.Select(x => x.MaVatTu));

        //            foreach (var vtLevel3 in listCap3)
        //            {
        //                var listCap4 = listVatTu.Where(x => x.MaCha == vtLevel3.MaVatTu).ToList();
        //                result.AddRange(listCap4.Select(x => x.MaVatTu));
        //            }
        //        }
        //    }

        //    return result;
        //}

        public static string ConvertLatinToLaMa(int value)
        {
            try
            {
                string strRet = string.Empty;
                Boolean _Flag = true;
                string[] ArrLama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
                int[] ArrNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
                int i = 0;
                while (_Flag)
                {
                    while (value >= ArrNumber[i])
                    {
                        value -= ArrNumber[i];
                        strRet += ArrLama[i];
                        if (value < 1)
                            _Flag = false;
                    }
                    i++;
                }
                return strRet;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string LongestCommonSubstring(string str1, string str2)
        {
            int[,] lengths = new int[str1.Length + 1, str2.Length + 1];
            int maxLength = 0;
            int endIndex = 0;

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        lengths[i, j] = lengths[i - 1, j - 1] + 1;

                        if (lengths[i, j] > maxLength)
                        {
                            maxLength = lengths[i, j];
                            endIndex = i - 1;
                        }
                    }
                    else
                    {
                        lengths[i, j] = 0;
                    }
                }
            }

            if (maxLength == 0)
            {
                return "";
            }

            return str1.Substring(endIndex - maxLength + 1, maxLength);
        }

        public static bool checkTonTaiMaNguonVon(string strQueryNguonVon, string strNguonVon)
        {
            var listQueryMaNguonVon = strQueryNguonVon.Split(',');
            var listMaNguonVon = strNguonVon.Split(',');

            foreach(var queryMaNguonVon in listQueryMaNguonVon)
            {
                if(listMaNguonVon.Contains(queryMaNguonVon))
                {
                    return true;
                }
            }
            return false;
        }

        public static string convertSoSangChu(int i)
        {
            string[] arr = { "A", "B", "C", "D", "E" };
            return arr[i-1];
        }

        private string toXML(dynamic x)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(x.GetType());
            serializer.Serialize(stringwriter, x);
            return stringwriter.ToString();
        }
    }
}