using FluentAssertions;
using System;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class StaffMeetingsShould
    {
        [Fact]
        public void NotShowOverlapWhenThereIsNoOverlap()
        {
            var format = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat;

            var meetings = new[]
            {
                    new Meeting(DateTime.Parse("1/1/2015 20:00", format), DateTime.Parse("1/1/2015 21:30", format)),
                    new Meeting(DateTime.Parse("1/1/2015 21:30", format), DateTime.Parse("1/1/2015 23:00", format)),
                    new Meeting(DateTime.Parse("1/1/2015 23:10", format), DateTime.Parse("1/1/2015 23:30", format))
            };
            var result = meetings.DoesNotOverlap();
            result.Should().BeTrue();
        }
    }
}
