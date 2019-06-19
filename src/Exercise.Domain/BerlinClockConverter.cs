using System;
using System.Text;
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
            var hours = System.Convert.ToInt16(timeParts[0]);
            var result = new StringBuilder();
            result.AppendLine(IsEven(seconds) ? "R" : "O");
            result.AppendLine();
            result.AppendLine(GetFirstRow(hours));
            return result.ToString();
        }

        private string GetFirstRow(short hours)
        {
            var result = new StringBuilder("OOOO");
            for (int i = 0; i < hours/5; i++)
            {
                result.Replace("O", "R", i, 1);
            }

            return result.ToString();
        }

        private static bool IsEven(short seconds)
        {
            return seconds % 2 == 0;
        }
    }
}