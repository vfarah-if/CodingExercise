using System;

namespace Exercise.Domain
{
    public class HoursConverter
    {
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