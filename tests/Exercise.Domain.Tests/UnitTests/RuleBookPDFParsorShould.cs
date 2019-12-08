using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class RuleBookPDFParsorShould
    {
        private const string tableOfContentsPdfPath = "TestData\\Table of Contents.pdf";
        private RuleBookPDFParsor ruleBookPDFParsor;

        public RuleBookPDFParsorShould()
        {
            ruleBookPDFParsor = new RuleBookPDFParsor();
        }

        [Fact]
        public void ExtractPDFDataAsHtml()
        {
            string testFile = Path.Combine(Environment.CurrentDirectory, tableOfContentsPdfPath);

            var result = ruleBookPDFParsor.ToHtml(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ExtractPDFDataAsText()
        {
            string testFile = Path.Combine(Environment.CurrentDirectory, tableOfContentsPdfPath);

            var result = ruleBookPDFParsor.ToText(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ExtractPDFDataAsXml()
        {
            string testFile = Path.Combine(Environment.CurrentDirectory, tableOfContentsPdfPath);

            var result = ruleBookPDFParsor.ToXml(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ExtractPDFDataAsWordText()
        {
            string testFile = Path.Combine(Environment.CurrentDirectory, tableOfContentsPdfPath);

            var result = ruleBookPDFParsor.ToWordAsString(testFile);

            result.Should().NotBeNullOrEmpty();
        }
    }
}
