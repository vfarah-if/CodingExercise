using System;
using System.Text.RegularExpressions;

namespace Exercise.Domain
{
    internal class TimeParts
    {
        internal static TimeParts Parse(string time)
        {
            return new TimeParts(time);
        }

        public TimeParts(string time)
        {
            if (string.IsNullOrEmpty(time))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(time));
            }
            ValidateTimeFormat(time);
            Initialise(time);
        }

        public short Minutes { get; private set; }
        public short Hours { get; private set; }
        public short Seconds { get; private set; }

        private void Initialise(string time)
        {
            var timeParts = time.Split(":");
            Seconds = System.Convert.ToInt16(timeParts[2]);
            Hours = System.Convert.ToInt16(timeParts[0]);
            Minutes = System.Convert.ToInt16(timeParts[1]);
        }

        private static void ValidateTimeFormat(string time)
        {
            Regex regex = new Regex(@"^(\d\d:\d\d:\d\d)$");
            if (!regex.IsMatch(time))
            {
                throw new NotSupportedException("Time should be in the expected 'hh:mm:ss' format");
            }
        }
    }
}
