using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerlinClock.Core.Interfaces;

namespace BerlinClock.Core.Model
{
    public class OneHourDiode : BerlinClockDiode
    {
        public static readonly TimeSpan OneHour = new TimeSpan(1, 0, 0);

        public override TimeSpan Time
        {
            get
            {
                return this.IsOn ? OneHour : TimeSpan.Zero;
            }
        }

        public OneHourDiode(DiodeColor color, bool isOn) : base(color, isOn)
        {
        }
    }
}
