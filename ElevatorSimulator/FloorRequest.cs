using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator
{
    public class FloorRequest
    {
        public int To { get; set; }
        public int From { get; set; }
        public bool GoingUp { get; set; }
        public bool GoingDown { get; set; }

        public bool IsGoingUp => From < To  || GoingUp;
        public bool IsGoingDown => From > To || GoingDown; 

        public bool Completed { get; set; }
    }

}
