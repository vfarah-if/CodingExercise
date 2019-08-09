using System.Text;
using static Exercise.Domain.Hours;
using static Exercise.Domain.Minutes;
using static Exercise.Domain.Seconds;

namespace Exercise.Domain
{
    public class BerlinClockConverter
    {
        protected readonly HoursConverter HoursConverter;
        protected readonly SecondsConverter SecondsConverter;
        protected readonly MinutesConverter MinutesConverter;

        public BerlinClockConverter()
            :this(new HoursConverter(), new SecondsConverter(), new MinutesConverter())
        {
        }

        protected BerlinClockConverter(
            HoursConverter hoursConverter, 
            SecondsConverter secondsConverter, 
            MinutesConverter minutesConverter)
        {
            HoursConverter = hoursConverter;
            SecondsConverter = secondsConverter;
            MinutesConverter = minutesConverter;
        }

        public string Convert(string time)
        {
            var timeParts = TimeParts.Parse(time);
            var result = new StringBuilder();
//          // Procedural Solution
//          result.AppendLine(SecondsConverter.GetSecondsClock(timeParts.Seconds)).AppendLine();
//          result.AppendLine(HoursConverter.GetFiveHoursClocks(timeParts.Hours)).AppendLine();
//          result.AppendLine(HoursConverter.GetSingleHoursClocks(timeParts.Hours)).AppendLine();
//          result.AppendLine(MinutesConverter.GetFiveMinutesClocks(timeParts.Minutes)).AppendLine();
//          result.Append(MinutesConverter.GetSingleMinutesClocks(timeParts.Minutes));
            
            // Object Oriented Solution
            result.Append(NewSeconds(timeParts.Seconds));
            result.Append(NewHours(timeParts.Hours));
            result.Append(NewMinutes(timeParts.Minutes));
            return result.ToString();
        }
    }
}