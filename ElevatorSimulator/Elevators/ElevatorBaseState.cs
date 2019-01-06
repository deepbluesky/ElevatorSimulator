using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    class ElevatorBaseState : IElevatorState
    {
        public Elevator Elevator { get; set; }

        public virtual void CloseDoor()
        {

        }

        public virtual void GoDown()
        {

        }

        public virtual void GoUp()
        {

        }

        public virtual void OpenDoor()
        {

        }

        public virtual void Stop() { }
    }
}
