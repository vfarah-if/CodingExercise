namespace Exercise.Domain
{
    public class MinutesConverter
    {
        public string GetSingleMinutesClocks(short minutes)
        {
            var amountOfLightsToSwitchOn = minutes % 5;
            return MultipleLightsBuilder.GetLightsRow(
                numberOfLightsToTurnOn: amountOfLightsToSwitchOn,
                numberOfLights: 4,
                onLight: Constants.YellowLight
            );
        }

        public string GetFiveMinutesClocks(short minutes)
        {
            var amountOfLightsToSwitchOn = minutes / 5;
            return MultipleLightsBuilder.GetLightsRow(
                numberOfLightsToTurnOn: amountOfLightsToSwitchOn,
                numberOfLights: 11,
                onLight: Constants.YellowLight
            );
        }
    }
}
