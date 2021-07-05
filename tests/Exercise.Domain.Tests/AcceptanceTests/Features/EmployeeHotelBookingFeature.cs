using System;
using CoreBDD;
using CoreBDD.SpecGeneration;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    [Feature("Employee Hotel Bookings",
        @"As an Employee
        I want to book a hotel room")]
    public class EmployeeHotelBookingFeature : Specification, IDisposable
    {
        public void Dispose()
        {
            GenerateSpecs.OutputFeatureSpecs(this.GetType().Assembly.Location, @"..\..\..\Specs\");
        }
    }
}
