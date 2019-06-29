using System.Text;

namespace Exercise.Domain
{
    public class Minutes: TimePart
    {
        public Minutes(short minutes): base(minutes)
        {}

        protected override string GetLights(short unit)
        {
            var result = new StringBuilder();
            result.AppendLine(GetFiveMinutesLights(unit)).AppendLine();
            result.Append(GetSingleMinutesLights(unit));
            return result.ToString();
        }

        private string GetSingleMinutesLights(short minutes)
        {
            var amountOfLightsToSwitchOn = minutes % 5;
            return GetLightsRow(
                numberOfLightsToTurnOn: amountOfLightsToSwitchOn,
                numberOfLights: 4,
                onLight: Constants.YellowLight);
        }

        private string GetFiveMinutesLights(short minutes)
        {
            var amountOfLightsToSwitchOn = minutes / 5;
            return GetLightsRow(
                numberOfLightsToTurnOn: amountOfLightsToSwitchOn,
                numberOfLights: 11,
                onLight: Constants.YellowLight);
        }

        public static Minutes NewMinutes(short minutes)
        {
            return new Minutes(minutes);
        }
    }
}
