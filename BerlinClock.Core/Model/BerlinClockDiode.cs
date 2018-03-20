using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerlinClock.Core.Interfaces;

namespace BerlinClock.Core.Model
{
    public abstract class BerlinClockDiode : ITimeRepresentation
    {
        public BerlinClockDiode(DiodeColor color, bool isOn)
        {
            this.Color = color;
            this.IsOn = isOn;
        }

        public DiodeColor Color { get; private set; }

        public bool IsOn { get; private set; }

        public bool ChangeState()
        {
            this.IsOn = !IsOn;
            return this.IsOn;
        }

        public void ChangeStateToOff()
        {
            this.IsOn = false;
        }

        public abstract TimeSpan Time { get; }
    }
}
