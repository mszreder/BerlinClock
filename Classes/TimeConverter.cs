using BerlinClock.Core.Interfaces;
using BerlinClock.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
   
    public class TimeConverter : ITimeConverter
    {
        ITimeTextInputConverter inputConverterService;
        IBerlineClockService berlinClockService;
        private IBerlinClockVisualisationService visualisationService;

        public TimeConverter(ITimeTextInputConverter inputConverterService, IBerlineClockService berlinClockService, IBerlinClockVisualisationService visualisationService)
        {
            this.berlinClockService = berlinClockService;
            this.visualisationService = visualisationService;
            this.inputConverterService = inputConverterService;
        }

        public string convertTime(string aTime)
        {
            TimeSpan time = this.inputConverterService.Convert(aTime);
            BerlinClockTimeRepresentation timeRepresentation = this.berlinClockService.RepresentTime(time);
            return this.visualisationService.TextVisualisation(timeRepresentation);
        }
    }
}
