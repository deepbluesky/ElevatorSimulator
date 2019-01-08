using ElevatorSimulator.Elevators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator.Commands
{
    public class GoToCommand : ICommand<int>
    {
        private IElevator Elevator { get;  set; }
        private Action _action;
        public int Data { get; set; }
        public GoToCommand(IElevator elevator, int floorNumber)
        {
            Data = floorNumber;
            Elevator = elevator;
            _action = () => { Elevator.RequestElevator(new FloorRequest {From = Elevator.CurrentFloor.FloorNumber, To = floorNumber }); };
        }

        public void Execute()
        {
            _action();
        }
    }
}
