using System;
using static Exercise.Domain.Constants;

namespace Exercise.Domain
{
    public class MinutesConverter
    {
        private static readonly Lazy<MinutesConverter> Lazy = new Lazy<MinutesConverter>(() => new MinutesConverter());

        static MinutesConverter()
        { }

        private MinutesConverter()
        { }

        public static MinutesConverter Instance => Lazy.Value;

        public string GetSingleMinutesClocks(short minutes)
        {
            var max = minutes % 5;
            return MultipleLightsConverter.Convert(max, 4, YellowLight);
        }

        public string GetFiveMinutesClocks(short minutes)
        {
            var max = minutes / 5;
            return MultipleLightsConverter.Convert(max, 11, YellowLight);
        }
    }
}