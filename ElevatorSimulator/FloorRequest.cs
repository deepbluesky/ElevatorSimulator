using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator
{
    public class FloorRequest
    {
        public int To { get; set; }
        public int From { get; set; }

        public bool IsGoingUp => From < To;
        public bool IsGoingDown => From > To; 

        public bool Completed { get; set; }
    }
}
