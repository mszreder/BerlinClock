using System;
using BerlinClock.Core.Interfaces;
using BerlinClock.Core.Services;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BerlinClock.Core.Tests
{
    [TestFixture]
    public class TextTimeFormatInputConversionServiceTests
    {
        private ITimeTextInputConverter textInputConverter;

        [Test]
        public void WhenInputIsNullExpectArgumentException()
        {
            string input = null;
            textInputConverter = new TextTimeFormatInputConversionService();
            Assert.Throws(typeof(ArgumentException), () => textInputConverter.Convert(input));
        }

        [Test]
        public void WhenInputIsEmptyExpectArgumentException()
        {
            string input = string.Empty;
            textInputConverter = new TextTimeFormatInputConversionService();
            Assert.Throws(typeof(ArgumentException), () => textInputConverter.Convert(input));
        }

        [Test]
        public void WhenInputInIncorrectFormatExpectArgumentException()
        {
            string input = "SD:rr,ewewe";
            textInputConverter = new TextTimeFormatInputConversionService();
            Assert.Throws(typeof(ArgumentException), () => textInputConverter.Convert(input));
        }

        [Test]
        public void WhenInputInCorrectFormatButHourBiggerThan24ExpectArgumentException()
        {
            string input = "25:11:11";
            textInputConverter = new TextTimeFormatInputConversionService();
            Assert.Throws(typeof(ArgumentException), () => textInputConverter.Convert(input));
        }

        [Test]
        public void WhenInputInCorrectFormatButMinutesBiggerThan59ExpectArgumentException()
        {
            string input = "13:60:11";
            textInputConverter = new TextTimeFormatInputConversionService();
            Assert.Throws(typeof(ArgumentException), () => textInputConverter.Convert(input));
        }

        [Test]
        public void WhenInputInCorrectFormatButSecondsBiggerThant59ExpectArgumentException()
        {
            string input = "13:30:60";
            textInputConverter = new TextTimeFormatInputConversionService();
            Assert.Throws(typeof(ArgumentException), () => textInputConverter.Convert(input));
        }

        [Test]
        public void WhenInputInCorrectFormatGetTimeSpanRepresentingPassesAsInputTime()
        {
            string input = "13:25:32";
            textInputConverter = new TextTimeFormatInputConversionService();
            TimeSpan result = textInputConverter.Convert(input);

            Assert.AreEqual("13",result.Hours.ToString());
            Assert.AreEqual("25", result.Minutes.ToString());
            Assert.AreEqual("32", result.Seconds.ToString());
        }
    }
}