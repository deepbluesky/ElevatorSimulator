using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    class ElevatorStopState : ElevatorBaseState
    {
        public override void GoDown()
        {
            if (Elevator.TargetFloor == BuildingFactory.NoFloor) return;
            Elevator.GoDownImpl();
            Elevator.State = Elevator.GoingDownState;
        }

        public override void GoUp()
        {
            if (Elevator.TargetFloor == BuildingFactory.NoFloor) return;
            Elevator.GoUpImpl();
            Elevator.State = Elevator.GoingUpState;
        }

        public override void OpenDoor()
        {
            Elevator.OpenDoor();
            Elevator.State = Elevator.DoorOpenState;
        }

        public override void CloseDoor()
        {
            Elevator.CloseDoor();
            Elevator.State = Elevator.DoorClosedState;
        }
    }
}
