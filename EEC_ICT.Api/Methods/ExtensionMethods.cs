using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEC_ICT.Api.Methods
{
    public static class ExtensionMethods
    {
        public static string Slugify(this String s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            sb = sb.Replace(' ', '_');
            return sb.ToString().Normalize(NormalizationForm.FormD);
        }
        public static List<T> AddRangeNotDuplicate<T>(this List<T> ls, List<T> lsIn)
        {
            for(var j = 0; j<lsIn.Count; j++)
            {
                if (!ls.Contains(lsIn[j]))
                {
                    ls.Add(lsIn[j]);
                }
            }
            return ls;
        }
    }
}