using System;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class RuleBookPDFParsorShould
    {
        private RuleBookPDFParsor ruleBookPDFParsor;

        [Fact]
        public void ExtractPDFDataAsPlainText()
        {
            ruleBookPDFParsor = new RuleBookPDFParsor();
            string testFile = "TestData/Table of Contents.pdf";
            var result = ruleBookPDFParsor.Parse(testFile);
            result.Should().NotBeNullOrEmpty();
        }
    }
}
