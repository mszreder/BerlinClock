using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerlinClock.Core.Interfaces;

namespace BerlinClock.Core.Model
{
    public class OneMinuteDiode : BerlinClockDiode
    {
        public static readonly TimeSpan OneMinute = new TimeSpan(0, 1, 0);

        public override TimeSpan Time
        {
            get { return this.IsOn ? OneMinute : TimeSpan.Zero; }
        }

        public OneMinuteDiode(DiodeColor color, bool isOn) : base(color, isOn)
        {
        }
    }
}
