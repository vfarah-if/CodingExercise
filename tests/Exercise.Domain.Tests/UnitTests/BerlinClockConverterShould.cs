using System;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

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

            action.Should().Throw<NotSupportedException>();
        }
    }
}
