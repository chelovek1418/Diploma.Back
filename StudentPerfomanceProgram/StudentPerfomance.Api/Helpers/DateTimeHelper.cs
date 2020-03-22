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
    }
}
