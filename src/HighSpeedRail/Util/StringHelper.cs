using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HighSpeedRail.Util
{
  /// <summary>
    /// string utility
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 轉換為Unix時間戳記
        /// </summary>
        /// <param name="dtTime">時間點</param>
        /// <returns></returns>
        public static int ConvertDateTimeToUnixInt(DateTime dtTime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

            return Convert.ToInt32((dtTime - dtStart).TotalSeconds);
        }

        /// <summary>
        /// MD5 32位加密
        /// </summary>
        /// <param name="sPlainText">要加密的字串</param>
        /// <param name="EncType">編碼方式</param>
        /// <returns></returns>
        public static string Get32Md5Str(string sPlainText, Encoding EncType)
        {
            return ConvertByteToHex(MD5.Create().ComputeHash(EncType.GetBytes(sPlainText)));
        }

        /// <summary>
        /// MD5 16位加密 
        /// </summary>
        /// <param name="sPlainText">待加密字串</param>
        /// <param name="EncType">編碼方式</param>
        /// <returns></returns>
        public static string Get16Md5Str(string sPlainText, Encoding EncType)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string sTemp = BitConverter.ToString(md5.ComputeHash(EncType.GetBytes(sPlainText)), 4, 8);
            sTemp = sTemp.Replace("-", "");
            return sTemp;
        }

        /// <summary>
        /// Btye轉換為Hex 16進制
        /// </summary>
        /// <param name="bytArray"></param>
        /// <returns></returns>
        public static string ConvertByteToHex(byte[] bytArray)
        {
            StringBuilder sb = new StringBuilder(bytArray.Length * 3);

            foreach (byte bytParam in bytArray)
            {
                sb.Append(Convert.ToString(bytParam, 16).PadLeft(2, '0'));
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// 16進位字串轉byte[]
        /// </summary>
        /// <param name="hex">HEX 字串</param>
        /// <returns></returns>
        public static byte[] ConvertStringFromHex(string hex)
        {
            string src = string.Empty;
            int len = hex.Length / 2;
            byte[] buffer = new byte[len];
            for (int i = 0; i < len; i++)
            {
                buffer[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return buffer;
        }

        /// <summary>
        /// 密碼遮罩
        /// </summary>
        /// <param name="sPassWord">密碼</param>
        /// <param name="sMask">遮罩 ex: *****</param>
        /// <param name="iRightCount">右邊剩餘顯示字數</param>
        /// <returns></returns>
        public static string PassWordMask(string sPassWord, string sMask, int iRightCount)
        {
            return System.Text.RegularExpressions.Regex.Replace(sPassWord, @".{" + sMask.Length.ToString() + "}.{" + iRightCount.ToString() + "}$", sMask + sPassWord.Substring(sPassWord.Length - iRightCount, iRightCount));
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string ToBase64(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string FromBase64(this string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

    }
}