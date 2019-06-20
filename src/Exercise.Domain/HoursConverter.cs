using System;

namespace Exercise.Domain
{
    public class HoursConverter
    {
        private static readonly Lazy<HoursConverter> Lazy = new Lazy<HoursConverter>(() => new HoursConverter());

        static HoursConverter()
        { }

        private HoursConverter()
        { }

        public static HoursConverter Instance => Lazy.Value;

        public string GetSingleHoursClocks(short hours)
        {
            var max = hours % 5;
            return MultipleLightsConverter.Convert(max, 4);
        }

        public string GetFiveHoursClocks(short hours)
        {
            var max = hours / 5;
            return MultipleLightsConverter.Convert(max, 4);
        }
    }
}