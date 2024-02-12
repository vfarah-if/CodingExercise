using System.Text;

namespace Exercise.Domain
{
    public class Seconds(short seconds) : TimePart(seconds)
    {
        protected override string GetLights(short unit)
        {
            var result = new StringBuilder();
            var amountOfLightsToTurnOn = IsEven(unit) ? 1 : 0;
            result.AppendLine(GetLightsRow(amountOfLightsToTurnOn, 1)).AppendLine();
            return result.ToString();
        }

        private static bool IsEven(short seconds)
        {
            return seconds % 2 == 0;
        }

        public static Seconds NewSeconds(short seconds)
        {
            return new Seconds(seconds);
        }
    }
}
