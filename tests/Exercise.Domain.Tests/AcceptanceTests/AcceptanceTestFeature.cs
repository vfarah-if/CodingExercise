using System;
using CoreBDD;
using CoreBDD.SpecGeneration;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    [Feature("Coding Exercise",
        @"As a dev idiot
        I want to make sure this works")]
    public class AcceptanceTestFeature : Specification, IDisposable
    {
        public void Dispose()
        {
            GenerateSpecs.OutputFeatureSpecs(this.GetType().Assembly.Location, @"..\..\..\Specs\");
        }
    }
}
