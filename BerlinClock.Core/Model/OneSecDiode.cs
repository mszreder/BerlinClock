using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerlinClock.Core.Interfaces;

namespace BerlinClock.Core.Model
{
    public class OneSecDiode : BerlinClockDiode
    {
        public override TimeSpan Time
        {
            get { return this.IsOn ? new TimeSpan(0, 0, 1) : new TimeSpan(0, 0, 0); }
        }

        public OneSecDiode(DiodeColor color, bool isOn) : base(color, isOn)
        {
        }
    }
}
