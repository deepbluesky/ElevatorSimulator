using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    public interface IElevator
    {
        void OpenDoor();
        void CloseDoor();
        void GoUp();
        void GoDown();
        void Stop();
        Floor CurrentFloor { get; set; }
        bool IsGoingUp();
        bool IsGoingDown();
        bool IsIdle();
    }
}
