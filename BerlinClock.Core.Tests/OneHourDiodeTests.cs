using System;
using BerlinClock.Core.Model;
using NUnit.Framework;

namespace BerlinClock.Core.Tests
{
    [TestFixture]
    public class OneHourDiodeTests
    {
        [Test]
        public void WhenDiodeIsOnTimeRepresentedEquals1Hour()
        {
            BerlinClockDiode diode = new OneHourDiode(DiodeColor.Yellow, true);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(new TimeSpan(1, 0, 0), representedTime);
        }

        [Test]
        public void WhenDiodeIsOffTimeRepresentedIsZero()
        {
            BerlinClockDiode diode = new OneHourDiode(DiodeColor.Yellow, false);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(TimeSpan.Zero, representedTime);
        }
    }
}