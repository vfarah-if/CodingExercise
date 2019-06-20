using static Exercise.Domain.Constants;

namespace Exercise.Domain
{
    public class MinutesConverter
    {
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