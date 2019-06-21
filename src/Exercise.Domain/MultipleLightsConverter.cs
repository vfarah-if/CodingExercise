using System.Text;
using static Exercise.Domain.Constants;

namespace Exercise.Domain
{
    public class MultipleLightsConverter
    {
        private const char OffLightChar = 'O';

        public static string Convert(int numberOfLightsToTurnOn, short numberOfLights, string onLight = RedLight)
        {
            var result = new StringBuilder("".PadRight(numberOfLights, OffLightChar));         
            for (var i = 0; i < numberOfLightsToTurnOn; i++)
            {
                result.Replace(OffLight, onLight, i, 1);
            }

            return result.ToString();
        }
    }
}
