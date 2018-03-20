using System;
using BerlinClock.Core.Model;
using NUnit.Framework;

namespace BerlinClock.Core.Tests
{
    [TestFixture]
    public class FiveHourDiodeTests
    {
        [Test]
        public void WhenDiodeIsOnTimeRepresentedEquals5Hours()
        {
            BerlinClockDiode diode = new FiveHourDiode(DiodeColor.Yellow, true);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(new TimeSpan(5,0,0), representedTime);
        }

        [Test]
        public void WhenDiodeIsOffTimeRepresentedIsZero()
        {
            BerlinClockDiode diode = new FiveHourDiode(DiodeColor.Yellow, false);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(TimeSpan.Zero, representedTime);
        }
    }
}