using System;
using CoreBDD;
using CoreBDD.SpecGeneration;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    [Feature("Coding Exercise",
        @"As a dev
        I want to create a rulebook pdf reader that can extract test from a pdf")]
    public class AcceptanceTestFeature : Specification, IDisposable
    {
        public void Dispose()
        {
            GenerateSpecs.OutputFeatureSpecs(this.GetType().Assembly.Location, @"..\..\..\Specs\");
        }
    }
}
