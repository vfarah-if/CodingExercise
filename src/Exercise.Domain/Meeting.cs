using System;

namespace Exercise.Domain
{
    public class Meeting
    {
        public Meeting(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; }
        public DateTime End { get; }
    }
}
