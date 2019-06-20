using System.Text;

namespace Exercise.Domain
{
    public class BerlinClockConverter
    {
        public string Convert(string time)
        {
            var timeParts = TimeParts.Parse(time);
            var result = new StringBuilder();
            result.AppendLine(SecondsConverter.Instance.GetSecondsClock(timeParts.Seconds)).AppendLine();
            result.AppendLine(HoursConverter.Instance.GetFiveHoursClocks(timeParts.Hours)).AppendLine();
            result.AppendLine(HoursConverter.Instance.GetSingleHoursClocks(timeParts.Hours)).AppendLine();
            result.AppendLine(MinutesConverter.Instance.GetFiveMinutesClocks(timeParts.Minutes)).AppendLine();
            result.Append(MinutesConverter.Instance.GetSingleMinutesClocks(timeParts.Minutes));
            return result.ToString();
        }
    }
}