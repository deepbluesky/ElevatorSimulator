using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator
{
    public interface IElevatorFloorConsole
    {
        void GoUp();
        void GoDown();

    }

    public interface IElevatorConsole : IElevatorFloorConsole
    {

        void OpenDoor();
        void CloseDoor();
        List<FloorRequest> FloorButtons { get; set; }
    }
}
