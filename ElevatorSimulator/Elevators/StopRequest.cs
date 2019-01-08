using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    public class StopRequest
    {
        public int FloorNumber { get; set; }
        public bool UpRequest { get; set; }
        public bool DownRequest { get; set; }
    }
}
