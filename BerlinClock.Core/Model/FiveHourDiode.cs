using BerlinClock.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Core.Model
{
    public class FiveHourDiode : BerlinClockDiode
    {
        public static readonly TimeSpan FiveHours = new TimeSpan(5, 0, 0);

        public override TimeSpan Time
        {
            get
            {
                return this.IsOn ? FiveHours : TimeSpan.Zero;
            }
        }

        public FiveHourDiode(DiodeColor color, bool isOn) : base(color, isOn)
        {
        }
    }
}
