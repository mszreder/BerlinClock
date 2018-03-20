using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BerlinClock.Core.Services;

namespace BerlinClock
{
    [Binding]
    public class TheBerlinClockSteps
    {
        private ITimeConverter berlinClock = new TimeConverter(
            new TextTimeFormatInputConversionService(), 
            new BerlineClockService(new BerlineClockFactory()), 
            new BerlinClockVisualisationService());
        private String theTime;

        
        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(string time)
        {
            theTime = time;
        }
        
        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            string result = berlinClock.convertTime(theTime);
            Assert.AreEqual(result, theExpectedBerlinClockOutput);
        }

    }
}
