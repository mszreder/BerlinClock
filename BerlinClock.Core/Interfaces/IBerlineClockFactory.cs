using BerlinClock.Core.Model;

namespace BerlinClock.Core.Interfaces
{
    public interface IBerlineClockFactory
    {
        BerlinClockTimeRepresentation BuildNew();
    }
}