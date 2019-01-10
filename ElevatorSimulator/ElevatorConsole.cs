using ElevatorSimulator.Elevators;
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
            //if (_downClicked) return;
            _downClicked = true;
            Controller.RequestElevator(Floor.FloorNumber, Floor.FloorNumber, false, true);
        }

        public void GoUp()
        {
            //if (_upClicked) return;
            _upClicked = true;
            Controller.RequestElevator(Floor.FloorNumber, Floor.FloorNumber, true);
        }

        public IElevatorController Controller { get; set; }
        public Floor Floor { get; set; }
    }

    public class ElevatorConsole : IElevatorConsole
    {
        public List<ICommand<int>> FloorButtons { get; set; }

        public void CloseDoor()
        {
            Elevator.CloseDoor();
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
            Elevator.OpenDoor();
        }

        public IElevatorController Controller { get; set; }
        public Floor Floor { get; set; }

        public IElevator Elevator { get; set; }

        public void RequestFloor(int floorNumber)
        {
            FloorButtons.Find(x => x.Data == floorNumber)?.Execute();
        }
    }
}
