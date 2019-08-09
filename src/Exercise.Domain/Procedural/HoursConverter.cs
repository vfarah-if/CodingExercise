namespace Exercise.Domain
{
    public class HoursConverter
    {
        public string GetSingleHoursClocks(short hours)
        {
            var amountOfLightsToSwitchOn = hours % 5;
            return MultipleLightsBuilder.GetLightsRow(numberOfLightsToTurnOn: amountOfLightsToSwitchOn, numberOfLights: 4);
        }

        public string GetFiveHoursClocks(short hours)
        {
            var amountOfLightsToSwitchOn = hours / 5;
            return MultipleLightsBuilder.GetLightsRow(amountOfLightsToSwitchOn, 4);
        }
    }
}