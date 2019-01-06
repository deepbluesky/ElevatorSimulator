using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    public interface IElevatorState
    {
        Elevator Elevator {get; set;}
        void OpenDoor();
        void CloseDoor();
        void GoUp();
        void GoDown();
        void Stop();
    }
}
