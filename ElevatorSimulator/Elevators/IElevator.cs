using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    public interface IElevator
    {
        String Name { get; set; }
        void OpenDoor();
        void CloseDoor();
        void GoUp();
        void GoDown();
        void Stop();
        Floor CurrentFloor { get; set; }
        bool IsGoingUp();
        bool IsGoingDown();
        bool IsIdle();
        void RequestElevator(FloorRequest req);
        IElevatorConsole Console { get; set; }
        List<StopRequest> StopRequests { get; }
        bool IsDoorClosed { get; }
        bool IsDoorOpened { get; }
    }
}
