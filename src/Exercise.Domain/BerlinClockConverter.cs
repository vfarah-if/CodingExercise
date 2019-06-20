using System.Text;

namespace Exercise.Domain
{
    public class BerlinClockConverter
    {
        private const string YellowLight = "Y";
        private const string OffLightRowOfFour = "OOOO";
        private const string OffLightRowOfEleven = "OOOOOOOOOOO";
        private const string RedLight = "R";
        private const string OffLight = "O";
        

        public string Convert(string time)
        {
            var timeParts = TimeParts.Parse(time);
            var result = new StringBuilder();
            result.AppendLine(GetTopClock(timeParts.Seconds)).AppendLine();
            result.AppendLine(GetFirstRow(timeParts.Hours)).AppendLine();
            result.AppendLine(GetSecondRow(timeParts.Hours)).AppendLine();
            result.AppendLine(GetThirdRow(timeParts.Minutes)).AppendLine();
            result.Append(GetFourthRow(timeParts.Minutes));
            return result.ToString();
        }

        private string GetFourthRow(short minutes)
        {
            var result = new StringBuilder(OffLightRowOfFour);
            var max = minutes % 5;
            for (var i = 0; i < max; i++)
            {
                result.Replace(OffLight, YellowLight, i, 1);
            }

            return result.ToString();
        }

        private string GetThirdRow(short minutes)
        {
            var result = new StringBuilder(OffLightRowOfEleven);
            var max = minutes / 5;
            for (var i = 0; i < max; i++)
            {
                result.Replace(OffLight, YellowLight, i, 1);
            }

            return result.ToString();
        }

        private static string GetTopClock(short seconds)
        {
            return IsEven(seconds) ? RedLight : OffLight;
        }

        private string GetSecondRow(short hours)
        {
            var result = new StringBuilder(OffLightRowOfFour);
            var max = hours % 5;
            for (var i = 0; i < max; i++)
            {
                result.Replace(OffLight, RedLight, i, 1);
            }

            return result.ToString();
        }

        private string GetFirstRow(short hours)
        {
            var result = new StringBuilder(OffLightRowOfFour);
            var max = hours/5;
            for (var i = 0; i < max; i++)
            {
                result.Replace(OffLight, RedLight, i, 1);
            }

            return result.ToString();
        }

        private static bool IsEven(short seconds)
        {
            return seconds % 2 == 0;
        }
    }
}