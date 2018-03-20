using BerlinClock.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Core.Interfaces
{
    public interface IBerlineClockService
    {
        BerlinClockTimeRepresentation RepresentTime(TimeSpan timeInput);
    }
}
