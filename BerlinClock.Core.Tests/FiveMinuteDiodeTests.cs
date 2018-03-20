using System;
using BerlinClock.Core.Model;
using NUnit.Framework;

namespace BerlinClock.Core.Tests
{
    [TestFixture]
    public class FiveMinuteDiodeTests
    {
        [Test]
        public void WhenDiodeIsOnTimeRepresentedEquals5Minutes()
        {
            BerlinClockDiode diode = new FiveMinuteDiode(DiodeColor.Yellow, true);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(new TimeSpan(0, 5, 0), representedTime);
        }

        [Test]
        public void WhenDiodeIsOffTimeRepresentedIsZero()
        {
            BerlinClockDiode diode = new FiveMinuteDiode(DiodeColor.Yellow, false);
            TimeSpan representedTime = diode.Time;
            Assert.AreEqual(TimeSpan.Zero, representedTime);
        }
    }
}