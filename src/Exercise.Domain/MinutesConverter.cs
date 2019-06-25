using static Exercise.Domain.Constants;
using static Exercise.Domain.MultipleLightsConfigurer;

namespace Exercise.Domain
{
    public class MinutesConverter
    {
        public string GetSingleMinutesClocks(short minutes)
        {
            var amountOfLightsToSwitchOn = minutes % 5;
            return GetLightConfiguration(
                numberOfLightsToTurnOn: amountOfLightsToSwitchOn, 
                numberOfLights: 4, 
                onLight: YellowLight);
        }

        public string GetFiveMinutesClocks(short minutes)
        {
            var amountOfLightsToSwitchOn = minutes / 5;
            return GetLightConfiguration(
                numberOfLightsToTurnOn: amountOfLightsToSwitchOn, 
                numberOfLights: 11, 
                onLight: YellowLight);
        }
    }
}