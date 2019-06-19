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
        [InlineData("12:56:01", "0")]
        [InlineData("12:56:02", "R")]
        public void ConvertSecondsToTheExpectedFormat(string time, string expectedFormat)
        {
            var actual = _berlinClockConverter.Convert(time);

            actual.Should().StartWith(expectedFormat);
        }
    }
}
