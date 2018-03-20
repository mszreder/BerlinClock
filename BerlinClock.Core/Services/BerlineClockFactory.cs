using BerlinClock.Core.Interfaces;
using BerlinClock.Core.Model;

namespace BerlinClock.Core.Services
{
    public class BerlineClockFactory : IBerlineClockFactory
    {
        public BerlinClockTimeRepresentation BuildNew()
        {
            return new BerlinClockTimeRepresentation();
        }
    }
}