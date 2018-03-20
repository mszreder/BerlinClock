using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BerlinClock;
using BerlinClock.Core.Interfaces;
using BerlinClock.Core.Model;
using Moq;

namespace BerlinClock.Tests
{
    [TestFixture]
    public class TimeConverterTests
    {
        private ITimeConverter timeConverter;
        Mock<ITimeTextInputConverter> inputConverterServiceMock;
        Mock<IBerlineClockService> berlinClockServiceMock;
        Mock<IBerlinClockVisualisationService> berlinClockVisualisationServiceMock;
        [Test]
        public void StringInputConvertedAndBerlinClockRepresentationCalculatedAndVisualised()
        {
            this.berlinClockServiceMock = new Mock<IBerlineClockService>();
            this.inputConverterServiceMock = new Mock<ITimeTextInputConverter>();
            this.berlinClockVisualisationServiceMock = new Mock<IBerlinClockVisualisationService>();
            this.timeConverter = new TimeConverter(this.inputConverterServiceMock.Object, this.berlinClockServiceMock.Object, this.berlinClockVisualisationServiceMock.Object);
            string inputParam = "10:10:10";
            TimeSpan mockTimeSpan = new TimeSpan();
            this.inputConverterServiceMock.Setup(x => x.Convert(inputParam)).Returns(mockTimeSpan);
            
            Mock<BerlinClockTimeRepresentation> timeRepresentationMock = new Mock<BerlinClockTimeRepresentation>();
            string fakeStringRepresentation = "Fake";
            berlinClockVisualisationServiceMock.Setup(x => x.TextVisualisation(timeRepresentationMock.Object))
                .Returns(fakeStringRepresentation);

            this.berlinClockServiceMock.Setup(x => x.RepresentTime(mockTimeSpan))
                .Returns(timeRepresentationMock.Object);

            string result = this.timeConverter.convertTime(inputParam);

            Assert.AreEqual(fakeStringRepresentation, result);
        }

        [Test]
        public void AnyExceptionThrownAtTimeInputConversionIsPropagated()
        {
            this.berlinClockServiceMock = new Mock<IBerlineClockService>();
            this.inputConverterServiceMock = new Mock<ITimeTextInputConverter>();
            this.berlinClockVisualisationServiceMock = new Mock<IBerlinClockVisualisationService>();
            this.timeConverter = new TimeConverter(this.inputConverterServiceMock.Object, this.berlinClockServiceMock.Object, this.berlinClockVisualisationServiceMock.Object);

            Exception thrown = new Exception();
            this.inputConverterServiceMock.Setup(x => x.Convert(It.IsAny<string>())).Throws(thrown);

            Assert.Throws(thrown.GetType(), () => timeConverter.convertTime("dd"));
        }

        [Test]
        public void AnyExceptionThrownAtTimeRepresentationCalculationIsPropagated()
        {
            this.berlinClockServiceMock = new Mock<IBerlineClockService>();
            this.inputConverterServiceMock = new Mock<ITimeTextInputConverter>();
            this.berlinClockVisualisationServiceMock = new Mock<IBerlinClockVisualisationService>();
            this.timeConverter = new TimeConverter(this.inputConverterServiceMock.Object, this.berlinClockServiceMock.Object, this.berlinClockVisualisationServiceMock.Object);

            this.inputConverterServiceMock.Setup(x => x.Convert(It.IsAny<string>())).Returns(new TimeSpan());

            Exception thrown = new Exception();
            this.berlinClockServiceMock.Setup(x => x.RepresentTime(It.IsAny<TimeSpan>())).Throws(thrown);

            Assert.Throws(thrown.GetType(), () => timeConverter.convertTime("dd"));
        }

        [Test]
        public void AnyExceptionAtVisualisationStepIsPropagated()
        {
            this.berlinClockServiceMock = new Mock<IBerlineClockService>();
            this.inputConverterServiceMock = new Mock<ITimeTextInputConverter>();
            this.berlinClockVisualisationServiceMock = new Mock<IBerlinClockVisualisationService>();
            this.timeConverter = new TimeConverter(this.inputConverterServiceMock.Object, this.berlinClockServiceMock.Object, this.berlinClockVisualisationServiceMock.Object);

            this.inputConverterServiceMock.Setup(x => x.Convert(It.IsAny<string>())).Returns(new TimeSpan());

            Exception thrown = new Exception();
            this.berlinClockServiceMock.Setup(x => x.RepresentTime(It.IsAny<TimeSpan>()))
                .Returns(new BerlinClockTimeRepresentation());

            this.berlinClockVisualisationServiceMock
                .Setup(x => x.TextVisualisation(It.IsAny<BerlinClockTimeRepresentation>())).Throws(thrown);

            Assert.Throws(thrown.GetType(), () => timeConverter.convertTime("dd"));
        }
    }
}
