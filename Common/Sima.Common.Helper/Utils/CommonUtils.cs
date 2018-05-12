using System;
using System.Text.RegularExpressions;
using Sima.Common.Constant;
using Sima.Common.Model.Types;

namespace Sima.Common.Helper.Utils
{
    public static class CommonUtils
    {
        public static object CreateErrorResponse(object request, Exception ex)
        {
            return new SimaErrorResponse
            {
                Status = (int)CommonStatus.UndefinedError,
                Message = ex.Message
            };
        }


        public static string ConvertStringToHex(string asciiString)
        {
            var hex = "";
            foreach (var c in asciiString)
            {
                int tmp = c;
                hex += $"{Convert.ToUInt32(tmp.ToString()):x2}";
            }
            return hex;
        }

        public static string StripVietnameseSigns(string str)
        {

            for (var i = 1; i < VietnameseSigns.Length; i++)
            {

                for (var j = 0; j < VietnameseSigns[i].Length; j++)
                {
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
            }

            return str;

        }

        public static string GenerateSlug(this string phrase, string glue = "-")
        {
            var str = StripVietnameseSigns(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-\.]", "");
            str = Regex.Replace(str, @"[\.]", " ");
            str = Regex.Replace(str, @"[-]", " ");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", glue); // hyphens   

            return str;
        }

        private static readonly string[] VietnameseSigns =
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };
    }
}