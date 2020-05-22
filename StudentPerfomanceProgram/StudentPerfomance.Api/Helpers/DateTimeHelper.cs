using System;

namespace StudentPerfomance.Api.Helpers
{
    internal static class DateTimeHelper
    {
        internal static DateTime GetTermStartDate()
        {
            var today = DateTime.Today;
            return today.Month >= 9 ? new DateTime(today.Year, 9, 1) : new DateTime(today.Year, 1, 1);
        }

        internal static DateTime GetNearestEducationalMonthStartDate()
        {
            var today = DateTime.Today;
            return today.Month > 5 && today.Month < 9 ? new DateTime(today.Year, 5, 1) : new DateTime(today.Year, today.Month, 1);
        }

        internal static string CheckStartDate(DateTime dateTime)
        {
            string error = null;

            if (dateTime > DateTime.Now)
                error = "Start date cannot be greater then current time";
            else if (dateTime < DateTime.Today.AddYears(-1))
                error = "Start date cannot be less then year ago";

            return error;
        }

        internal static int GetCurrentSemestr() => DateTime.Today.Month >= 8 ? 1 : 0; 
    }
}
