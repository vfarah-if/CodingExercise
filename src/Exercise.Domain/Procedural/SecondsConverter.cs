namespace Exercise.Domain
{
    public class SecondsConverter
    {
        public string GetSecondsClock(short seconds, string onLight = Constants.RedLight)
        {
            return IsEven(seconds) ? onLight : Constants.OffLight;
        }

        private static bool IsEven(short seconds)
        {
            return seconds % 2 == 0;
        }
    }
}