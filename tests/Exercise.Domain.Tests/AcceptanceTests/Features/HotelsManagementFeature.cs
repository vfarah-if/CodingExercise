using System;
using CoreBDD;
using CoreBDD.SpecGeneration;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    [Feature("Hotel Rooms And Quantities",
        @"As a Hotel Manager
        I want to set all the different types of rooms and respective quantities for my hotel")]
    public class HotelsManagementFeature : Specification, IDisposable
    {
        public void Dispose()
        {
            GenerateSpecs.OutputFeatureSpecs(this.GetType().Assembly.Location, @"..\..\..\Specs\");
        }
    }
}
