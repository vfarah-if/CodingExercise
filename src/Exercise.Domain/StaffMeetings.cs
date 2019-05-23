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

            // Solve this problem
            Meeting previousMeeting = null;
            foreach (var meeting in meetings.OrderBy(x => x.Start))
            {
                if (previousMeeting != null && previousMeeting.End > meeting.Start)
                {
                    return false;
                }

                previousMeeting = meeting;
            }

            return true;
        }
    }


}
