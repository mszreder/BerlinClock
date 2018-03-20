using System.Linq;
using BerlinClock.Core.Model;
using NUnit.Framework;

namespace BerlinClock.Core.Tests
{
    [TestFixture]
    public class BerlinClockTimeRepresentationTests
    {
        /// <summary>
        /// Four FiveHourDiodes
        /// Four OneHourDiodes
        /// 11 Five Minut Diodes
        /// Four 1 minute diodes
        /// One Top Diode as OneSecondDiode
        /// </summary>
        [Test]
        public void DefaultConstructorCreatesDiodesInCorrectNumberAndType()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            Assert.AreEqual(4, timeRepresentation.FirstRow.Length);
            Assert.IsTrue(timeRepresentation.FirstRow.All(x => x is FiveHourDiode));
            Assert.AreEqual(4, timeRepresentation.SecondRow.Length);
            Assert.IsTrue(timeRepresentation.SecondRow.All(x => x is OneHourDiode));
            Assert.AreEqual(11, timeRepresentation.ThirdRow.Length);
            Assert.IsTrue(timeRepresentation.ThirdRow.All(x => x is FiveMinuteDiode));
            Assert.AreEqual(4, timeRepresentation.ForthRow.Length);
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => x is OneMinuteDiode));
            Assert.AreEqual(typeof(OneSecDiode), timeRepresentation.TopDiode.GetType());
        }

        [Test]
        public void DefaultConstructorCreatedDiodesHaveCorrectColors()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            Assert.IsTrue(timeRepresentation.FirstRow.All(x => x.Color == DiodeColor.Red));
            Assert.IsTrue(timeRepresentation.SecondRow.All(x => x.Color == DiodeColor.Red));
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => x.Color == DiodeColor.Yellow));
            Assert.IsTrue(timeRepresentation.TopDiode.Color == DiodeColor.Yellow);

            for (int i = 0; i < 11; i++)
            {
                if (i == 2 || i == 5 || i == 8)
                {
                    Assert.IsTrue(timeRepresentation.ThirdRow[i].Color == DiodeColor.Red, "3rd, 6th and 9th FiveMinuteDiodes should be red");
                }
                else
                {
                    Assert.IsTrue(timeRepresentation.ThirdRow[i].Color == DiodeColor.Yellow, "All except 3rd, 6th and 9th FiveMinuteDiodes should be yellow");
                }
            }
        }

        [Test]
        public void ResetClockSetStateOfAllDiodesToOff()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            foreach (var diode in timeRepresentation.FirstRow.Union(timeRepresentation.SecondRow).Union(timeRepresentation.ThirdRow).Union(timeRepresentation.ForthRow))
            {
                diode.ChangeState();
            }

            timeRepresentation.TopDiode.ChangeState();
            timeRepresentation.ResetClock();

            Assert.IsTrue(timeRepresentation.FirstRow.Union(timeRepresentation.SecondRow).Union(timeRepresentation.ThirdRow).Union(timeRepresentation.ForthRow).All(x => !x.IsOn));
            Assert.IsFalse(timeRepresentation.TopDiode.IsOn);
        }

        [Test]
        public void ZeroHoursAsSetHoursInputTurnOnNoDiodes()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            timeRepresentation.SetHours(0);

            Assert.IsTrue(timeRepresentation.FirstRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.SecondRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ThirdRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => !x.IsOn));
            Assert.IsTrue(!timeRepresentation.TopDiode.IsOn);
        }

        [Test]
        public void FiveHoursAsSetHoursInputTurnOnOneDiodeOnFirstRow()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            timeRepresentation.SetHours(5);

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    Assert.IsTrue(timeRepresentation.FirstRow[i].IsOn);
                }
                else
                {
                    Assert.IsFalse(timeRepresentation.FirstRow[i].IsOn);
                }
            }

            Assert.IsTrue(timeRepresentation.SecondRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ThirdRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => !x.IsOn));
        }

        [Test]
        public void TwentyFourHoursAsSetHoursInputTurnOnAllDiodesOnFirstAndSecondRow()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            timeRepresentation.SetHours(24);

            Assert.IsTrue(timeRepresentation.FirstRow.All(x => x.IsOn));
            Assert.IsTrue(timeRepresentation.SecondRow.All(x => x.IsOn));
            Assert.IsTrue(timeRepresentation.ThirdRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => !x.IsOn));
        }

        [Test]
        public void ZeroMinutesAsSetMinutesTurnsOnNoDiodesInAnyRow()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            timeRepresentation.SetMinutes(0);

            Assert.IsTrue(timeRepresentation.FirstRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.SecondRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ThirdRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => !x.IsOn));
        }

        [Test]
        public void FiftyNineMinutesAsSetMinutesTurnsOnAllDiodesOfThirdAndFourthRow()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            timeRepresentation.SetMinutes(59);

            Assert.IsTrue(timeRepresentation.FirstRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.SecondRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ThirdRow.All(x => x.IsOn));
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => x.IsOn));
        }

        [Test]
        public void ZeroAsSetSecondsTurnsOnOnlyTopDiode()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            timeRepresentation.SetSeconds(0);

            Assert.IsTrue(timeRepresentation.FirstRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.SecondRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ThirdRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.TopDiode.IsOn);
        }

        [Test]
        public void FiftyNineAsSetSecondsTurnsOnNoneOfDiodes()
        {
            BerlinClockTimeRepresentation timeRepresentation = new BerlinClockTimeRepresentation();
            timeRepresentation.SetSeconds(59);

            Assert.IsTrue(timeRepresentation.FirstRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.SecondRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ThirdRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentation.ForthRow.All(x => !x.IsOn));
            Assert.IsFalse(timeRepresentation.TopDiode.IsOn);
        }
    }
}