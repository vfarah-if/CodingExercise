using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class RuleBookPDFParsorShould
    {
        private const string tableOfContentsPdfRelativePath = @"TestData\Table of Contents.pdf";
        private const string linkSamplePdfRelativePath = @"TestData\LinkSample.pdf";

        private RuleBookPDFParsor ruleBookPDFParsor;

        public RuleBookPDFParsorShould()
        {
            ruleBookPDFParsor = new RuleBookPDFParsor();
        }

        [Fact]
        public void ExtractPDFDataAsHtml()
        {
            string testFile = GetFullPdfFilePath(tableOfContentsPdfRelativePath);

            var result = ruleBookPDFParsor.ToHtml(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ExtractPDFDataAsHtmlWithLinksWorking()
        {
            string testFile = GetFullPdfFilePath(linkSamplePdfRelativePath);

            var result = ruleBookPDFParsor.ToHtml(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ExtractPDFDataAsText()
        {
            string testFile = GetFullPdfFilePath(tableOfContentsPdfRelativePath);

            var result = ruleBookPDFParsor.ToText(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ExtractPDFDataAsXml()
        {
            string testFile = GetFullPdfFilePath(tableOfContentsPdfRelativePath);

            var result = ruleBookPDFParsor.ToXml(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ExtractPDFDataAsXmlWithLinksWorking()
        {
            string testFile = GetFullPdfFilePath(linkSamplePdfRelativePath);

            var result = ruleBookPDFParsor.ToXml(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ExtractPDFDataAsWordText()
        {
            string testFile = GetFullPdfFilePath(tableOfContentsPdfRelativePath);

            var result = ruleBookPDFParsor.ToWordAsString(testFile);

            result.Should().NotBeNullOrEmpty();
        }

        private static string GetFullPdfFilePath(string relativeFilePath)
        {
            return Path.Combine(Environment.CurrentDirectory, relativeFilePath);
        }
    }
}
