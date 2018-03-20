using System;
using BerlinClock.Core.Model;
using NUnit.Framework;

namespace BerlinClock.Core.Tests
{
    [TestFixture]
    public class OneSecondDiodeTests
    {
        [Test]
        public void WhenDiodeIsOnTimeRepresentedEquals1Minute()
        {
            BerlinClockDiode diode = new OneSecDiode(DiodeColor.Yellow, true);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(new TimeSpan(0, 0, 1), representedTime);
        }

        [Test]
        public void WhenDiodeIsOffTimeRepresentedIsZero()
        {
            BerlinClockDiode diode = new OneSecDiode(DiodeColor.Yellow, false);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(TimeSpan.Zero, representedTime);
        }
    }
}