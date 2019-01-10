using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Elevators
{
    class ElevatorGoingDownState : ElevatorBaseState
    {
        public override void Stop()
        {
            Elevator.StopImpl();
            Elevator.State = Elevator.StopState;
        }
    }
}
