using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator
{
    public class ElevatorFloorConsole : IElevatorFloorConsole
    {
        public void GoDown()
        {
            throw new NotImplementedException();
        }

        public void GoUp()
        {
            throw new NotImplementedException();
        }
    }

    public class ElevatorConsole : IElevatorConsole
    {
        public List<FloorRequest> FloorButtons { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void CloseDoor()
        {
            throw new NotImplementedException();
        }

        public void GoDown()
        {
            throw new NotImplementedException();
        }

        public void GoUp()
        {
            throw new NotImplementedException();
        }

        public void OpenDoor()
        {
            throw new NotImplementedException();
        }
    }
}
