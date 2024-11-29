using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;


namespace WebdocTerminal.Helpers
{
    public class DateFormatter
    {
        public static DateTime ConvertToEdiFormat(string date)
        {
            var date2 = date.Length > 12 ? DateTime.ParseExact(date, "yyyyMMddHHmmss", CultureInfo.InvariantCulture) : DateTime.ParseExact(date, "yyyyMMddHHmm", CultureInfo.InvariantCulture);

            return date2;
        }

        public static string ConvertToyyyyMMddFormat(DateTime date)
        {
            string year = date.Year.ToString();
            string month = (date.Month > 9 ? date.Month.ToString() : "0" + date.Month.ToString());
            string day = (date.Day > 9 ? date.Day.ToString() : "0" + date.Day.ToString());

            var dateString = year + month + day;

            return dateString;
        }

        public static string ConvertToHHmmFormat(DateTime date)
        {
            string hours = (date.Hour > 9 ? date.Hour.ToString() : "0" + date.Hour.ToString());
            string mins = (date.Minute > 9 ? date.Minute.ToString() : "0" + date.Minute.ToString());

            var dateString = hours + mins;

            return dateString;
        }

        public static string ConvertToyyyyMMddHHmmFormat(DateTime date)
        {
            string year = date.Year.ToString();
            string month = (date.Month > 9 ? date.Month.ToString() : "0" + date.Month.ToString());
            string day = (date.Day > 9 ? date.Day.ToString() : "0" + date.Day.ToString());
            string hours = (date.Hour > 9 ? date.Hour.ToString() : "0" + date.Hour.ToString());
            string mins = (date.Minute > 9 ? date.Minute.ToString() : "0" + date.Minute.ToString());

            var dateString = year + month + day + hours + mins;

            return dateString;
        }

        public static string ConvertToyyyyMMddHHmmssFormat(DateTime date)
        {
            string year = date.Year.ToString();
            string month = (date.Month > 9 ? date.Month.ToString() : "0" + date.Month.ToString());
            string day = (date.Day > 9 ? date.Day.ToString() : "0" + date.Day.ToString());
            string hours = (date.Hour > 9 ? date.Hour.ToString() : "0" + date.Hour.ToString());
            string mins = (date.Minute > 9 ? date.Minute.ToString() : "0" + date.Minute.ToString());
            string seconds = (date.Second > 9 ? date.Second.ToString() : "0" + date.Second.ToString());

            var dateString = year + month + day + hours + mins + seconds;

            return dateString;
        }
    }
}
