﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    class ElevatorOpenDoorState : ElevatorBaseState
    {
        
        public override void CloseDoor()
        {
            Elevator.CloseDoorImpl();
            Elevator.State = Elevator.DoorClosedState;
        } 
         
    }
}
