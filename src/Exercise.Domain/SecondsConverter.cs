using static Exercise.Domain.Constants;

namespace Exercise.Domain
{
    public class SecondsConverter
    {
        public string GetSecondsClock(short seconds, string onLight = RedLight)
        {
            return IsEven(seconds) ? onLight : OffLight;
        }

        private static bool IsEven(short seconds)
        {
            return seconds % 2 == 0;
        }
    }
}