using CoreBDD;
using FluentAssertions;
using System;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class BerlinClockShould : AcceptanceTestFeature
    {
        private BerlinClockConverter _berlinClockConverter;

        [Scenario("Format hh:mm:ss format to berlin clock format")]
        public void FormatHHMMSSToACustomBerlinClockFormat()
        {
            string time = null;
            string expected = TestScenarios.ExpectedFormat;
            string actual = null;
            Given("12:56:01 and a berlin clock converter", () =>
            {
                time = "12:56:01";
                _berlinClockConverter = new BerlinClockConverter();
            });
            When("converting the time", () =>
            {
                actual = _berlinClockConverter.Convert(time);
            });
            Then($"the converted output should be {Environment.NewLine}'{expected}'", () =>
            {
                actual.Should().Be(expected);
            });
        }
    }
}
