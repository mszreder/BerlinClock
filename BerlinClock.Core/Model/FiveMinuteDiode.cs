using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerlinClock.Core.Interfaces;

namespace BerlinClock.Core.Model
{
    public class FiveMinuteDiode : BerlinClockDiode
    {
        public static readonly TimeSpan FiveMinutes = new TimeSpan(0, 5, 0);

        public override TimeSpan Time
        {
            get
            {
                return IsOn ? FiveMinutes: TimeSpan.Zero;
            }
        }

        public FiveMinuteDiode(DiodeColor color, bool isOn) : base(color, isOn)
        {
        }
    }
}
