using System;
using static Exercise.Domain.Constants;

namespace Exercise.Domain
{
    public class SecondsConverter
    {
        private static readonly Lazy<SecondsConverter> Lazy = new Lazy<SecondsConverter>(() => new SecondsConverter());

        static SecondsConverter()
        {}

        private SecondsConverter()
        {}

        public static SecondsConverter Instance => Lazy.Value;

        public string GetSecondsClock(short seconds)
        {
            return IsEven(seconds) ? RedLight : OffLight;
        }

        private bool IsEven(short seconds)
        {
            return seconds % 2 == 0;
        }
    }
}