using System.Text;

namespace Exercise.Domain
{
    public class MultipleLightsBuilder
    {
        private const char OffLightChar = 'O';

        public static string GetLightsRow(int numberOfLightsToTurnOn, short numberOfLights, string onLight = Constants.RedLight)
        {
            var result = new StringBuilder("".PadRight(numberOfLights, OffLightChar));         
            for (var i = 0; i < numberOfLightsToTurnOn; i++)
            {
                result.Replace(Constants.OffLight, onLight, i, 1);
            }

            return result.ToString();
        }
    }
}
