using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Core.Interfaces
{
    public interface ITimeTextInputConverter
    {
        TimeSpan Convert(string input);
    }
}
