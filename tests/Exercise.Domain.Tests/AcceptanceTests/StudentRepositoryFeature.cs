using System;
using System.Collections.Generic;
using System.Text;
using CoreBDD;
using CoreBDD.SpecGeneration;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    [Feature("Student Editing",
        @"As a user
        I want to be able to edit users")]
    public class StudentRepositoryFeature : Specification, IDisposable
    {
        public void Dispose()
        {
            GenerateSpecs.OutputFeatureSpecs(this.GetType().Assembly.Location, @"..\..\..\Specs\");
        }
    }
}
