using System;
using BerlinClock.Core.Interfaces;
using BerlinClock.Core.Model;

namespace BerlinClock.Core.Services
{
    public class BerlineClockService : IBerlineClockService
    {
        /// <summary>
        /// 24:00:00 is one day
        /// </summary>
        private readonly TimeSpan OneFullDay = new TimeSpan(1,0,0,0);
        private readonly IBerlineClockFactory berlinClockFactory;

        public BerlineClockService(IBerlineClockFactory berlinClockFactory)
        {
            this.berlinClockFactory = berlinClockFactory;
        }

        public BerlinClockTimeRepresentation RepresentTime(TimeSpan timeInput)
        {
            if (timeInput.TotalHours > 24.0)
            {
                throw new ArgumentException(string.Format("Hours cannot exeed 24. Got {0}", timeInput));
            }

            BerlinClockTimeRepresentation timeRepresentation = berlinClockFactory.BuildNew();
            timeRepresentation.ResetClock();
            if (timeInput == OneFullDay)
            {
                timeRepresentation.SetHours(24);
            }
            else
            {
                timeRepresentation.SetHours(timeInput.Hours);
            }

            timeRepresentation.SetMinutes(timeInput.Minutes);
            timeRepresentation.SetSeconds(timeInput.Seconds);
            return timeRepresentation;
        }
    }
}