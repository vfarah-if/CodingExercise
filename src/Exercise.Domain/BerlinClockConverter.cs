using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Exercise.Domain
{
    public class BerlinClockConverter
    {
        private const string OffLightRowOfFour = "OOOO";
        private const string RedLight = "R";
        private const string OffLight = "O";

        public string Convert(string time)
        {
            ValidateTimeFormat(time);

            var timeParts = time.Split(':');
            var seconds = System.Convert.ToInt16(timeParts[2]);
            var hours = System.Convert.ToInt16(timeParts[0]);
            var result = new StringBuilder();
            result.AppendLine(IsEven(seconds) ? RedLight : OffLight);
            result.AppendLine();
            result.AppendLine(GetFirstRow(hours));
            result.AppendLine();
            result.AppendLine(GetSecondRow(hours));
            return result.ToString();
        }

        private string GetSecondRow(short hours)
        {
            var result = new StringBuilder(OffLightRowOfFour);
            for (int i = 0; i < hours % 5; i++)
            {
                result.Replace(OffLight, RedLight, i, 1);
            }

            return result.ToString();
        }

        private string GetFirstRow(short hours)
        {
            var result = new StringBuilder(OffLightRowOfFour);
            for (int i = 0; i < hours/5; i++)
            {
                result.Replace(OffLight, RedLight, i, 1);
            }

            return result.ToString();
        }

        private static void ValidateTimeFormat(string time)
        {
            Regex regex = new Regex(@"^(\d\d:\d\d:\d\d)$");
            if (!regex.IsMatch(time))
            {
                throw new NotSupportedException("Time should be in the expected 'hh:mm:ss' format");
            }
        }

        private static bool IsEven(short seconds)
        {
            return seconds % 2 == 0;
        }
    }
}