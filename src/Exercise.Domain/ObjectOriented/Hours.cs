using System.Text;

namespace Exercise.Domain
{
    public class Hours: TimePart
    {
        private readonly short _hours;

        public Hours(short hours)
        {
            _hours = hours;
        }

        public override string ToString()
        {
            return GetLights(_hours);
        }

        protected override string GetLights(short unit)
        {
            var result = new StringBuilder();
            result.AppendLine(GetFiveHoursLights(unit)).AppendLine();
            result.AppendLine(GetSingleHoursLights(unit)).AppendLine();
            return result.ToString();
        }

        private string GetFiveHoursLights(short hours)
        {
            var amountOfLightsToSwitchOn = hours / 5;
            return GetLightsRow(amountOfLightsToSwitchOn, 4);
        }

        private string GetSingleHoursLights(short hours)
        {
            var amountOfLightsToSwitchOn = hours % 5;
            return GetLightsRow(numberOfLightsToTurnOn: amountOfLightsToSwitchOn, numberOfLights: 4);
        }

        public static Hours NewHours(short hours)
        {
            return new Hours(hours);
        }
    }
}
