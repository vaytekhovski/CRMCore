using System;

namespace Business
{
    public class DatesToSession
    {
        public static string DateToSession(DateTime date)
        {
            string Month = date.Month < 10 ? $"0{date.Month}" : $"{date.Month}";
            string Day = date.Day < 10 ? $"0{date.Day}" : $"{date.Day}";

            return $"{date.Year}-{Month}-{Day}";
        }
    }
}
