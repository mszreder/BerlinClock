using System;
using System.Linq;
using BerlinClock.Core.Interfaces;
using BerlinClock.Core.Model;
using BerlinClock.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BerlinClock.Core.Tests
{
    [TestFixture]
    public class BerlinClockServiceTests
    {
        private IBerlineClockService berlinClockService;
        private Mock<IBerlineClockFactory> berlinClockFactoryMock;


        [Test]
        public void WhenInputExeed24HoursExpectArgumentException()
        {
            this.berlinClockFactoryMock = new Mock<IBerlineClockFactory>();
            berlinClockService = new BerlineClockService(this.berlinClockFactoryMock.Object);

            BerlinClockTimeRepresentation timeRepresentationFactoryProduct = new BerlinClockTimeRepresentation();
            this.berlinClockFactoryMock.Setup(x => x.BuildNew()).Returns(timeRepresentationFactoryProduct);

            Assert.Throws(typeof(ArgumentException), () => berlinClockService.RepresentTime(new TimeSpan(24, 0, 1)));

        }

        /// <summary>
        /// Tests 00:00:00
        /// </summary>
        [Test]
        public void InputZeroReturnsOnlyTopDiodeOn()
        {
            this.berlinClockFactoryMock = new Mock<IBerlineClockFactory>();
            berlinClockService = new BerlineClockService(this.berlinClockFactoryMock.Object);

            BerlinClockTimeRepresentation timeRepresentationFactoryProduct = new BerlinClockTimeRepresentation();
            this.berlinClockFactoryMock.Setup(x => x.BuildNew()).Returns(timeRepresentationFactoryProduct);

            BerlinClockTimeRepresentation timeRepresentationResult = berlinClockService.RepresentTime(TimeSpan.Zero);
            Assert.IsTrue(timeRepresentationResult.TopDiode.IsOn);
            Assert.IsTrue(timeRepresentationResult.FirstRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentationResult.SecondRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentationResult.ThirdRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentationResult.ForthRow.All(x => !x.IsOn));
        }

        /// <summary>
        /// Tests 23:59:59
        /// </summary>
        [Test]
        public void InputLastSecondOfDayReturnsAllDiodeOnExecptTopAndLastOfSecondRow()
        {
            this.berlinClockFactoryMock = new Mock<IBerlineClockFactory>();
            berlinClockService = new BerlineClockService(this.berlinClockFactoryMock.Object);

            BerlinClockTimeRepresentation timeRepresentationFactoryProduct = new BerlinClockTimeRepresentation();
            this.berlinClockFactoryMock.Setup(x => x.BuildNew()).Returns(timeRepresentationFactoryProduct);

            BerlinClockTimeRepresentation timeRepresentationResult = berlinClockService.RepresentTime(new TimeSpan(23, 59, 59));

            Assert.IsFalse(timeRepresentationResult.TopDiode.IsOn);

            Assert.IsTrue(timeRepresentationResult.SecondRow[0].IsOn);
            Assert.IsTrue(timeRepresentationResult.SecondRow[1].IsOn);
            Assert.IsTrue(timeRepresentationResult.SecondRow[2].IsOn);
            Assert.IsFalse(timeRepresentationResult.SecondRow[3].IsOn);

            Assert.IsTrue(timeRepresentationResult.FirstRow.All(x => x.IsOn));
            Assert.IsTrue(timeRepresentationResult.ThirdRow.All(x => x.IsOn));
            Assert.IsTrue(timeRepresentationResult.ForthRow.All(x => x.IsOn));
        }

        [Test]
        public void InputTwentyFourHoursZeroMinutesZeroSecondsTurnsOnDiodesOnFirstAndSecondRowsAndTopDiodeOnly()
        {
            this.berlinClockFactoryMock = new Mock<IBerlineClockFactory>();
            berlinClockService = new BerlineClockService(this.berlinClockFactoryMock.Object);

            BerlinClockTimeRepresentation timeRepresentationFactoryProduct = new BerlinClockTimeRepresentation();
            this.berlinClockFactoryMock.Setup(x => x.BuildNew()).Returns(timeRepresentationFactoryProduct);

            //24:00:00 is represented by 1 full day in timespan representation
            BerlinClockTimeRepresentation timeRepresentationResult = berlinClockService.RepresentTime(new TimeSpan(1,0, 0, 0));

            Assert.IsTrue(timeRepresentationResult.TopDiode.IsOn);
            
            Assert.IsTrue(timeRepresentationResult.FirstRow.All(x => x.IsOn));
            Assert.IsTrue(timeRepresentationResult.SecondRow.All(x => x.IsOn));
            Assert.IsTrue(timeRepresentationResult.ThirdRow.All(x => !x.IsOn));
            Assert.IsTrue(timeRepresentationResult.ForthRow.All(x => !x.IsOn));
        }

        [Test]
        public void Input190001ReturnsLastDiodeOfSecondRowOn()
        {
            this.berlinClockFactoryMock = new Mock<IBerlineClockFactory>();
            berlinClockService = new BerlineClockService(this.berlinClockFactoryMock.Object);

            BerlinClockTimeRepresentation timeRepresentationFactoryProduct = new BerlinClockTimeRepresentation();
            this.berlinClockFactoryMock.Setup(x => x.BuildNew()).Returns(timeRepresentationFactoryProduct);

            BerlinClockTimeRepresentation timeRepresentationResult = berlinClockService.RepresentTime(new TimeSpan(19, 00, 01));

            Assert.IsFalse(timeRepresentationResult.TopDiode.IsOn);
            Assert.IsTrue(timeRepresentationResult.SecondRow.Last().IsOn);
        }
    }
}