using System.Text;

namespace Exercise.Domain
{
    public abstract class TimePart
    {
        private const char OffLightChar = 'O';

        protected TimePart(short unit)
        {
            Unit = unit;
        }

        public short Unit { get;  }

        public override string ToString()
        {
            return GetLights(Unit);
        }

        protected abstract string GetLights(short unit);
        
        protected string GetLightsRow(int numberOfLightsToTurnOn, short numberOfLights, string onLight = Constants.RedLight)
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
