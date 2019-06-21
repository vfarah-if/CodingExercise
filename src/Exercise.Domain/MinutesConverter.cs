using static Exercise.Domain.Constants;

namespace Exercise.Domain
{
    public class MinutesConverter
    {
        public string GetSingleMinutesClocks(short minutes)
        {
            var amountOfLightsToSwitchOn = minutes % 5;
            return MultipleLightsConverter.Convert(amountOfLightsToSwitchOn, 4, YellowLight);
        }

        public string GetFiveMinutesClocks(short minutes)
        {
            var amountOfLightsToSwitchOn = minutes / 5;
            return MultipleLightsConverter.Convert(amountOfLightsToSwitchOn, 11, YellowLight);
        }
    }
}