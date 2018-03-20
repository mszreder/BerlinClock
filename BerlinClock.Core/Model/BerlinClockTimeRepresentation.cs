using BerlinClock.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Core.Model
{
    public class BerlinClockTimeRepresentation
    {
        public BerlinClockDiode[] FirstRow { get; private set; }

        public BerlinClockDiode[] SecondRow { get; private set; }

        public BerlinClockDiode[] ThirdRow { get; private set; }

        public BerlinClockDiode[] ForthRow { get; private set; }

        public BerlinClockDiode TopDiode { get; private set; }

        private IEnumerable<BerlinClockDiode> AllDiodes
        {
            get
            {
                foreach (var berlinClockDiode in FirstRow)
                {
                    yield return berlinClockDiode;
                }

                foreach (var berlinClockDiode in SecondRow)
                {
                    yield return berlinClockDiode;
                }

                foreach (var berlinClockDiode in ThirdRow)
                {
                    yield return berlinClockDiode;
                }

                foreach (var berlinClockDiode in ForthRow)
                {
                    yield return berlinClockDiode;
                }

                yield return TopDiode;
            }
        }

        private FiveHourDiode CreateFiveHourDiode(DiodeColor color)
        {
            return new FiveHourDiode(color, false);
        }

        private OneHourDiode CreateOneHourDiode(DiodeColor color)
        {
            return new OneHourDiode(color, false);
        }

        private FiveMinuteDiode CreateFiveMinuteDiode(DiodeColor color)
        {
            return new FiveMinuteDiode(color, false);
        }

        private OneMinuteDiode CreateOneMinuteDiode(DiodeColor color)
        {
            return new OneMinuteDiode(color, false);
        }

        private OneSecDiode CreateOneSecondDiode(DiodeColor color)
        {
            return new OneSecDiode(color, false);
        }
        
        public BerlinClockTimeRepresentation()
        {
            this.FirstRow = new BerlinClockDiode[4]
            {
                this.CreateFiveHourDiode(DiodeColor.Red),
                this.CreateFiveHourDiode(DiodeColor.Red),
                this.CreateFiveHourDiode(DiodeColor.Red),
                this.CreateFiveHourDiode(DiodeColor.Red)
            };

            this.SecondRow = new BerlinClockDiode[4]
            {
                this.CreateOneHourDiode(DiodeColor.Red),
                this.CreateOneHourDiode(DiodeColor.Red),
                this.CreateOneHourDiode(DiodeColor.Red),
                this.CreateOneHourDiode(DiodeColor.Red)
            };

            this.ThirdRow = new BerlinClockDiode[11]
            {
                this.CreateFiveMinuteDiode(DiodeColor.Yellow),
                this.CreateFiveMinuteDiode(DiodeColor.Yellow),
                this.CreateFiveMinuteDiode(DiodeColor.Red),
                this.CreateFiveMinuteDiode(DiodeColor.Yellow),
                this.CreateFiveMinuteDiode(DiodeColor.Yellow),
                this.CreateFiveMinuteDiode(DiodeColor.Red),
                this.CreateFiveMinuteDiode(DiodeColor.Yellow),
                this.CreateFiveMinuteDiode(DiodeColor.Yellow),
                this.CreateFiveMinuteDiode(DiodeColor.Red),
                this.CreateFiveMinuteDiode(DiodeColor.Yellow),
                this.CreateFiveMinuteDiode(DiodeColor.Yellow)
            };

            this.ForthRow = new BerlinClockDiode[4]
            {
                this.CreateOneMinuteDiode(DiodeColor.Yellow),
                this.CreateOneMinuteDiode(DiodeColor.Yellow),
                this.CreateOneMinuteDiode(DiodeColor.Yellow),
                this.CreateOneMinuteDiode(DiodeColor.Yellow)
            };
            this.TopDiode = this.CreateOneSecondDiode(DiodeColor.Yellow);
        }

        public void ResetClock()
        {
            foreach (var clockDiode in AllDiodes)
            {
               clockDiode.ChangeStateToOff();
            }
        }

        public void SetHours(int numberOfHours)
        {
            TimeSpan remainingTime = new TimeSpan(numberOfHours, 0, 0);
            int currentIndexOfDiode = 0;
            while (remainingTime >= FiveHourDiode.FiveHours)
            {
                this.FirstRow[currentIndexOfDiode].ChangeState();
                currentIndexOfDiode++;
                remainingTime = remainingTime - FiveHourDiode.FiveHours;
            }

            currentIndexOfDiode = 0;
            while (remainingTime >= OneHourDiode.OneHour)
            {
                SecondRow[currentIndexOfDiode].ChangeState();
                currentIndexOfDiode++;
                remainingTime = remainingTime - OneHourDiode.OneHour;
            }
        }

        public void SetMinutes(int numberOfMinutes)
        {
            TimeSpan remainingTime = new TimeSpan(0,numberOfMinutes, 0);
            int currentIndexOfDiode = 0;

            while (remainingTime >= FiveMinuteDiode.FiveMinutes)
            {
                ThirdRow[currentIndexOfDiode].ChangeState();
                currentIndexOfDiode++;
                remainingTime = remainingTime - FiveMinuteDiode.FiveMinutes;
            }

            currentIndexOfDiode = 0;
            while (remainingTime >= OneMinuteDiode.OneMinute)
            {
                ForthRow[currentIndexOfDiode].ChangeState();
                currentIndexOfDiode++;
                remainingTime = remainingTime - OneMinuteDiode.OneMinute;
            }
        }

        public void SetSeconds(int numberOfSeconds)
        {
            bool turnSecondDiodeOn = numberOfSeconds % 2 == 0;
            if (turnSecondDiodeOn)
            {
                TopDiode.ChangeState();
            }
        }
    }
}
