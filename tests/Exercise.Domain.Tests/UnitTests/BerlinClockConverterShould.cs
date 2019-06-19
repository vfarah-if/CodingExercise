using System;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class BerlinClockConverterShould
    {
        private BerlinClockConverter _berlinClockConverter;

        public BerlinClockConverterShould()
        {
            _berlinClockConverter = new BerlinClockConverter();
        }

        [Theory]  
        [InlineData("badformat")]
        [InlineData("00,00,00")]
        [InlineData(" 00:00:00 ")]
        [InlineData("00:00")]
        public void ThrowANotSupportedExceptionWhenTimeInBadFormat(string badTimeFormat)
        {
            Action action = () => _berlinClockConverter.Convert(badTimeFormat);

            action.Should().Throw<NotSupportedException>()
                .WithMessage("Time should be in the expected 'hh:mm:ss' format");
        }

        [Theory]
        [InlineData("00:00:00", "R")]
        [InlineData("12:56:01", "O")]
        [InlineData("12:56:02", "R")]
        public void ConvertSecondsToTheExpectedFormat(string time, string expectedFormat)
        {
            var actual = _berlinClockConverter.Convert(time);

            actual.Should().StartWith(expectedFormat);
        }

        [Theory]
        [InlineData("12:56:01", "RROO")]
        [InlineData("00:00:00", "OOOO")]
        [InlineData("23:59:59", "RRRR")]
        [InlineData("02:04:00", "OOOO")]
        [InlineData("08:23:00", "ROOO")]
        [InlineData("16:35:00", "RRRO")]
        public void ConvertHoursToTheExpectedFormat(string time, string expectedFormat)
        {
            var actual = _berlinClockConverter.Convert(time);

            var splitLines = actual.Split(Environment.NewLine);
            splitLines.Length.Should().BeGreaterOrEqualTo(2);
            splitLines[2].Should().StartWith(expectedFormat);
        }
    }
}
