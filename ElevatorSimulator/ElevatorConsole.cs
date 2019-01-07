using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator
{
    public class ElevatorFloorConsole : IElevatorFloorConsole
    {
        private bool _upClicked = false;
        private bool _downClicked = false;
        public void GoDown()
        {
            if (_downClicked) return;
            _downClicked = true;
        }

        public void GoUp()
        {
            if (_upClicked) return;
            _upClicked = true;
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
