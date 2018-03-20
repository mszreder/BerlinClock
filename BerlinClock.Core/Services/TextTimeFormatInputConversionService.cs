using System;
using System.Text.RegularExpressions;
using BerlinClock.Core.Interfaces;

namespace BerlinClock.Core.Services
{
    public class TextTimeFormatInputConversionService : ITimeTextInputConverter
    {
        public TimeSpan Convert(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input cannot be null or empty");
            }

            var match = Regex.Match(input, @"^([0-9]{2}):([0-9]{2}):([0-9]{2})$");
            if (match.Success)
            {
                string hoursStringValue = match.Groups[1].Value;
                string minutesStringValue = match.Groups[2].Value;
                string secondsStringValue = match.Groups[3].Value;
                int hours = int.Parse(hoursStringValue);
                int minutes = int.Parse(minutesStringValue);
                int seconds = int.Parse(secondsStringValue);

                if (hours > 24)
                {
                    throw new ArgumentException(string.Format("Cannot pass bigger number than 24 as hours"));
                }

                if (minutes > 59)
                {
                    throw new ArgumentException(string.Format("Cannot pass bigger number than 59 as minutes"));
                }

                if (seconds > 59)
                {
                    throw new ArgumentException(string.Format("Cannot pass bigger number than 59 as seconds"));
                }

                return new TimeSpan(hours, minutes, seconds);
            }
            else
            {
                throw new ArgumentException(string.Format("Incorrect input format. Expected: HH:MM:SS Got: {0}", input));
            }
        }
    }
}