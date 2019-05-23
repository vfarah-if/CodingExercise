using FluentAssertions;
using System;
using System.Globalization;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class StaffMeetingsShould
    {
        private readonly DateTimeFormatInfo _format;

        public StaffMeetingsShould()
        {
            _format = CultureInfo.InvariantCulture.DateTimeFormat;
        }

        [Fact]
        public void NotShowOverlapWhenThereIsNoOverlap()
        {
            var meetings = new[]
            {
                    new Meeting(DateTime.Parse("1/1/2015 20:00", _format), DateTime.Parse("1/1/2015 21:30", _format)),
                    new Meeting(DateTime.Parse("1/1/2015 21:30", _format), DateTime.Parse("1/1/2015 23:00", _format)),
                    new Meeting(DateTime.Parse("1/1/2015 23:10", _format), DateTime.Parse("1/1/2015 23:30", _format))
            };
            var result = meetings.DoesNotOverlap();
            result.Should().BeTrue();
        }

        [Fact]
        public void ShowOverlapWhenThereIsOverlap()
        {
            var meetings = new[]
            {
                new Meeting(DateTime.Parse("1/1/2015 20:00", _format), DateTime.Parse("1/1/2015 21:30", _format)),
                new Meeting(DateTime.Parse("1/1/2015 21:00", _format), DateTime.Parse("1/1/2015 23:00", _format)),
                new Meeting(DateTime.Parse("1/1/2015 10:10", _format), DateTime.Parse("1/1/2015 23:30", _format))
            };
            var result = meetings.DoesNotOverlap();
            result.Should().BeFalse();
        }
    }
}
