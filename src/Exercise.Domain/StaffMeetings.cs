using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Domain
{
    public static class StaffMeetings
    {
        public static bool DoesNotOverlap(this IEnumerable<Meeting> meetings)
        {
            if (meetings == null)
            {
                throw new ArgumentNullException(nameof(meetings));
            }

            Meeting previousMeeting = null;
            foreach (var meeting in meetings.OrderBy(x => x.Start))
            {
                if (previousMeeting.OverlapsWith(meeting))
                {
                    return false;
                }

                previousMeeting = meeting;
            }

            return true;
        }

        private static bool OverlapsWith(this Meeting previousMeeting, Meeting meeting)
        {
            return previousMeeting != null && previousMeeting.End > meeting.Start;
        }
    }


}
