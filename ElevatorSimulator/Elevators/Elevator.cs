using System;
using System.Collections.Generic;
using System.Text;
using ElevatorSimulator.Elevators;

namespace ElevatorSimulator
{
    public class Elevator
    { 
        public Floor CurrentFloor { get; set; } 
        public IElevatorConsole Console { get; set; }

        internal IElevatorState State { get; set; }


        internal IElevatorState GoingUpState = new ElevatorGoingUpState();
        internal IElevatorState GoingDownState = new ElevatorGoingDownState();
        internal IElevatorState DoorOpenState = new ElevatorOpenDoorState();
        internal IElevatorState DoorClosedState = new ElevatorCloseDoorState();
        internal IElevatorState StopState = new ElevatorStopState();

        public Elevator()
        {
            State = DoorClosedState;
        }

        public void OpenDoor()
        {
            State.OpenDoor();
        }
        public void CloseDoor()
        {
            State.CloseDoor();
        }
        public void GoUp()
        {
            State.GoUp();
        }
        public void GoDown()
        {
            State.GoDown();
        }

        public void Stop()
        {
            State.Stop();
        }

    }
}
