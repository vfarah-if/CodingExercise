using System.Text;
using static Exercise.Domain.Constants;

namespace Exercise.Domain
{
    public class MultipleLightsConverter
    {
        protected internal const char OffLightChar = 'O';

        public static string Convert(int max, short numberOfLights, string onLight = RedLight)
        {
            var result = new StringBuilder("".PadRight(numberOfLights, OffLightChar));         
            for (var i = 0; i < max; i++)
            {
                result.Replace(OffLight, onLight, i, 1);
            }

            return result.ToString();
        }
    }
}
