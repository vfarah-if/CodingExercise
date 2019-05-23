using System;
using System.Diagnostics;

namespace Exercise.Domain
{
    public class Meeting
    {
        public Meeting(DateTime start, DateTime end)
        {
            if (start > end)
            {
                Debug.WriteLine("Start and end dates swapped because the start date was greater than the end date", "warning");
                Start = end;
                End = start;
            }
            else
            {
                Start = start;
                End = end;
            }
        }

        public DateTime Start { get; }
        public DateTime End { get; }
    }
}
