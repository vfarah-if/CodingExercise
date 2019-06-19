using System;
using System.Diagnostics;
using CoreBDD;
using FluentAssertions;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class BerlinClockShouldShould: AcceptanceTestFeature
    {
        private BerlinClockConverter _berlinClockConverter;

        [Scenario("Format hh:mm:ss format to berlin clock format")]
        public void ValidateAnyExpectationsThroughTheScenario()
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
            Then("an expectation should be satisfied", () =>
            {
                actual.Should().Be(expected);
            });
        }
    }
}
