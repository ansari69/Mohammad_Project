using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MiniShop.Application.Common.Helpers
{
   public static class DateConvertor
    {
        public static DateTime ToIranTimeZone(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime,
                            TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time"));
        }

        public static string ToShamsiString(this DateTime dateTime)
        {
            if (dateTime.Year < 2000) return "";
            var pc = new PersianCalendar();
            return $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0')}";
        }

        public static string ToShamsiString(this DateTime? dateTime)
        {
            if (dateTime == null) return "";
            var pc = new PersianCalendar();
            return $"{pc.GetYear(dateTime.Value)}/{pc.GetMonth(dateTime.Value).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(dateTime.Value).ToString().PadLeft(2, '0')}";
        }




        public static string ToTehranTimeString(this DateTime dateTime)
        {
            var time = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time"));
            return $"{time.Hour.ToString().PadLeft(2, '0')}:{time.Minute.ToString().PadLeft(2, '0')}";
        }

        public static string ToTehranTimeString(this DateTime? dateTime)
        {
            if (dateTime == null) return "";
            var time = TimeZoneInfo.ConvertTime(dateTime.Value, TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time"));
            return $"{time.Hour.ToString().PadLeft(2, '0')}:{time.Minute.ToString().PadLeft(2, '0')}";
        }

        public static int GetShamsiDayOfYear(this DateTime dateTime)
        {
            if (dateTime.Year < 2000) return 0;
            var pc = new PersianCalendar();
            return pc.GetDayOfYear(dateTime);
        }

        public static int GetShamsiDayOfMonth(this DateTime dateTime)
        {
            if (dateTime.Year < 2000) return 0;
            var pc = new PersianCalendar();
            return pc.GetDayOfMonth(dateTime);
        }

        public static int GetShamsiYear(this DateTime dateTime)
        {
            if (dateTime.Year < 2000) return 0;
            var pc = new PersianCalendar();
            return pc.GetYear(dateTime);
        }

     
    }
}
