using System;
using BerlinClock.Core.Model;
using NUnit.Framework;

namespace BerlinClock.Core.Tests
{
    [TestFixture]
    public class OneMinuteDiodeTests
    {
        [Test]
        public void WhenDiodeIsOnTimeRepresentedEquals1Minute()
        {
            BerlinClockDiode diode = new OneMinuteDiode(DiodeColor.Yellow, true);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(new TimeSpan(0, 1, 0), representedTime);
        }

        [Test]
        public void WhenDiodeIsOffTimeRepresentedIsZero()
        {
            BerlinClockDiode diode = new OneMinuteDiode(DiodeColor.Yellow, false);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(TimeSpan.Zero, representedTime);
        }
    }
}