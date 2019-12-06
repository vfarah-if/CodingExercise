using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class RuleBookPDFParsorShould
    {
        private RuleBookPDFParsor ruleBookPDFParsor;

        [Fact]
        public void ExtractPDFDataAsHtmlInorderToParseContentIntoKnownModel()
        {
            ruleBookPDFParsor = new RuleBookPDFParsor();
            string testFile = Path.Combine(Environment.CurrentDirectory,"TestData\\Table of Contents.pdf");
            var result = ruleBookPDFParsor.ToHtml(testFile);
            result.Should().NotBeNullOrEmpty();
        }
    }
}
