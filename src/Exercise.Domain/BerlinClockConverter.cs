using System.Text;

namespace Exercise.Domain
{
    public class BerlinClockConverter
    {
        private readonly HoursConverter _hoursConverter;
        private readonly SecondsConverter _secondsConverter;
        private MinutesConverter _minutesConverter;

        public BerlinClockConverter()
            :this(new HoursConverter(), new SecondsConverter(), new MinutesConverter())
        {
        }

        protected BerlinClockConverter(HoursConverter hoursConverter, SecondsConverter secondsConverter, MinutesConverter minutesConverter)
        {
            _hoursConverter = hoursConverter;
            _secondsConverter = secondsConverter;
            _minutesConverter = minutesConverter;
        }

        public string Convert(string time)
        {
            var timeParts = TimeParts.Parse(time);
            var result = new StringBuilder();
            result.AppendLine(_secondsConverter.GetSecondsClock(timeParts.Seconds)).AppendLine();
            result.AppendLine(_hoursConverter.GetFiveHoursClocks(timeParts.Hours)).AppendLine();
            result.AppendLine(_hoursConverter.GetSingleHoursClocks(timeParts.Hours)).AppendLine();
            result.AppendLine(_minutesConverter.GetFiveMinutesClocks(timeParts.Minutes)).AppendLine();
            result.Append(_minutesConverter.GetSingleMinutesClocks(timeParts.Minutes));
            return result.ToString();
        }
    }
}