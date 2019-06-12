using System;
using System.Diagnostics;
using CoreBDD;
using FluentAssertions;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class CompanyAdminShould: CompanyAdminFeature
    {
        [Scenario("Test Scenario Should ...")]
        public void ValidateAnyExpectationsThroughTheScenario()
        {
            Given("a scenario", () =>
            {
                Debug.WriteLine("Should setup an acceptance test scenario");
            });
            When("a condition is set", () =>
            {
                Debug.WriteLine("Should setup an acceptance test scenario");
            });
            Then("an expectation should be satisfied", () =>
            {
                Debug.WriteLine("Verify");
                true.Should().BeTrue();
            });
        }
    }
}
