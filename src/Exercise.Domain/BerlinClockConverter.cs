using System.Text;
using static Exercise.Domain.Hours;
using static Exercise.Domain.Minutes;
using static Exercise.Domain.Seconds;

namespace Exercise.Domain
{
    public class BerlinClockConverter
    {
        protected readonly HoursConverter hoursConverter;
        protected readonly SecondsConverter secondsConverter;
        protected readonly MinutesConverter minutesConverter;

        public BerlinClockConverter()
            : this(new HoursConverter(), new SecondsConverter(), new MinutesConverter()) { }

        private BerlinClockConverter(
            HoursConverter hoursConverter,
            SecondsConverter secondsConverter,
            MinutesConverter minutesConverter
        )
        {
            this.hoursConverter = hoursConverter;
            this.secondsConverter = secondsConverter;
            this.minutesConverter = minutesConverter;
        }

        public string Convert(string time)
        {
            var timeParts = TimeParts.Parse(time);
            var result = new StringBuilder();
            // Object Oriented Solution
            result.Append(NewSeconds(timeParts.Seconds));
            result.Append(NewHours(timeParts.Hours));
            result.Append(NewMinutes(timeParts.Minutes));
            return result.ToString();
        }
    }
}
