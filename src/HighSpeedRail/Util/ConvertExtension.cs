using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Util
{
    public static class ConvertExtension
    {
        public static int ParseIntOrDefault(this string str)
        {
            int result;
            return int.TryParse(str, out result) ? result : 0;
        }

        public static long ParseDecimalToCentsOrDefault(this decimal amount)
        {
            return (long)(amount * 100);
        }

        public static decimal ParseDecimalFromCentsOrDefault(this decimal amount)
        {
            return amount / 100;
        }

        public static bool ParseBoolOrDefault(this string str)
        {
            bool result;
            return bool.TryParse(str, out result) ? result : false;
        }

        public static decimal ParseDecimalOrDefault(this string str)
        {
            decimal result;
            return decimal.TryParse(str, out result) ? result : 0;

        }

        public static long ParseLongOrDefault(this string str)
        {
            long result;
            return long.TryParse(str, out result) ? result : 0;

        }

        public static DateTime ParseDateTimeOrDefault(this string str)
        {
            DateTime result;
            return DateTime.TryParse(str, out result) ? result : DateTime.MinValue;
        }

    }
}