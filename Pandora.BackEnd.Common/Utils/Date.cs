using System.Globalization;

namespace Pandora.BackEnd.Common.Utils
{
    public static class DateHelper
    {
        public static string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }
    }
}
