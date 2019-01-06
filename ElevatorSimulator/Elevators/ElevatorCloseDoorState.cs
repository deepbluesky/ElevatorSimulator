using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    class ElevatorCloseDoorState : ElevatorBaseState
    {
        public override void GoDown()
        {
            Elevator.GoDown();
            Elevator.State = Elevator.GoingDownState;
        }

        public override void GoUp()
        {
            Elevator.GoUp();
            Elevator.State = Elevator.GoingUpState;
        }

        public override void OpenDoor()
        {
            Elevator.OpenDoor();
            Elevator.State = Elevator.DoorOpenState;
        }
    }
}
