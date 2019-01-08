using ElevatorSimulator.Elevators;
using System;
using System.Collections.Generic;
using System.Text;


namespace ElevatorSimulator
{
    public interface IElevatorFloorConsole
    {
        IElevatorController Controller { get; set; }
        void GoUp();
        void GoDown();
        Floor Floor { get; set; }

    }

    public interface IElevatorConsole : IElevatorFloorConsole
    {

        void OpenDoor();
        void CloseDoor();
        List<ICommand<int>> FloorButtons { get; set; }
        IElevator Elevator { get; set; }
    }
}
