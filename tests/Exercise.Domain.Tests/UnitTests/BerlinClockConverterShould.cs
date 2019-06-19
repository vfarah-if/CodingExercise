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

        [Fact]  
        public void ThrowANotSupportedExceptionWhenFormattedNotExpected()
        {
            Action action = () => _berlinClockConverter.Convert("badformat");

            action.Should().Throw<NotSupportedException>();
        }
    }
}
