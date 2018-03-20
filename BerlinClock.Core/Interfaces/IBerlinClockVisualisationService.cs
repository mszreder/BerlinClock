using BerlinClock.Core.Model;

namespace BerlinClock.Core.Interfaces
{
    public interface IBerlinClockVisualisationService
    {
        string TextVisualisation(BerlinClockTimeRepresentation berlinClock);
    }
}