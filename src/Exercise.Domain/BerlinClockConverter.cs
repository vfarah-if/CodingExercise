using System;
using System.Text.RegularExpressions;

namespace Exercise.Domain
{
    public class BerlinClockConverter
    {
        public string Convert(string time)
        {
            Regex regex = new Regex(@"^(\d\d:\d\d:\d\d)$");
            if (!regex.IsMatch(time))
            {
                throw new NotSupportedException("Time should be in the expected 'hh:mm:ss' format");
            }

            var timeParts = time.Split(':');
            var seconds = System.Convert.ToInt16(timeParts[2]);
            return IsEven(seconds) ? "R" : "0";
        }

        private static bool IsEven(short seconds)
        {
            return seconds % 2 == 0;
        }
    }
}