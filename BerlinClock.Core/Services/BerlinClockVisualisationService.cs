using System;
using System.Collections.Generic;
using System.Text;
using BerlinClock.Core.Interfaces;
using BerlinClock.Core.Model;

namespace BerlinClock.Core.Services
{
    public class BerlinClockVisualisationService : IBerlinClockVisualisationService
    {
        public string TextVisualisation(BerlinClockTimeRepresentation berlinClock)
        {
            StringBuilder returnFormat = new StringBuilder();
            returnFormat.AppendLine(ColorStringRepresentation(berlinClock.TopDiode.Color, berlinClock.TopDiode.IsOn));
            returnFormat.AppendLine(string.Concat(DiodesLine(berlinClock.FirstRow)));
            returnFormat.AppendLine(string.Concat(DiodesLine(berlinClock.SecondRow)));
            returnFormat.AppendLine(string.Concat(DiodesLine(berlinClock.ThirdRow)));
            returnFormat.AppendLine(string.Concat(DiodesLine(berlinClock.ForthRow)));
            string resultString = returnFormat.ToString().TrimEnd();
            return resultString;
        }
        
        private string ColorStringRepresentation(DiodeColor color, bool isOn)
        {
            if (!isOn)
            {
                return "O";
            }

            switch (color)
            {
                case DiodeColor.Yellow:
                    return "Y";
                case DiodeColor.Red:
                    return "R";
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }

        private IEnumerable<string> DiodesLine(BerlinClockDiode[] diodes)
        {
            foreach (var berlinClockDiode in diodes)
            {
                yield return ColorStringRepresentation(berlinClockDiode.Color, berlinClockDiode.IsOn);
            }
        }
    }
}