using System.Diagnostics;
using System.Text;

namespace Exercise.Domain
{
    public class ProceduralBerlinClockConverter
    {
        private readonly HoursConverter hoursConverter;
        private readonly MinutesConverter minutesConverter;
        private readonly SecondsConverter secondsConverter;

        public ProceduralBerlinClockConverter()
            : this(new HoursConverter(), new MinutesConverter(), new SecondsConverter()) { }

        private ProceduralBerlinClockConverter(
            HoursConverter hoursConverter,
            MinutesConverter minutesConverter,
            SecondsConverter secondsConverter
        )
        {
            Debug.Assert(hoursConverter != null, nameof(hoursConverter) + " != null");
            this.hoursConverter = hoursConverter;
            Debug.Assert(minutesConverter != null, nameof(minutesConverter) + " != null");
            this.minutesConverter = minutesConverter;
            Debug.Assert(secondsConverter != null, nameof(this.secondsConverter) + " != null");
            this.secondsConverter = secondsConverter;
        }

        public string Convert(string time)
        {
            var timeParts = TimeParts.Parse(time);
            var result = new StringBuilder();
            result.AppendLine(secondsConverter.GetSecondsClock(timeParts.Seconds)).AppendLine();
            result.AppendLine(hoursConverter.GetFiveHoursClocks(timeParts.Hours)).AppendLine();
            result.AppendLine(hoursConverter.GetSingleHoursClocks(timeParts.Hours)).AppendLine();
            result
                .AppendLine(minutesConverter.GetFiveMinutesClocks(timeParts.Minutes))
                .AppendLine();
            result.Append(minutesConverter.GetSingleMinutesClocks(timeParts.Minutes));
            return result.ToString();
        }
    }
}
