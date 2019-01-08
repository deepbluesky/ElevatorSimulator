using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ElevatorSimulator.Elevators;

namespace ElevatorSimulator
{
    public class Building
    {
        private List<Floor> _floors;

        private List<IElevator> _elevators;

        public IElevatorController Controller { get; set; }

        public Building(int basements, int floors, int elevators = 0)
        {
            _floors = new List<Floor>();
            _elevators = new List<IElevator>();
        }

        public List<IElevator> Elevators { get => _elevators; set { _elevators = value; SetupElevatorConsoles(); } }
        public List<Floor> Floors { get => _floors; set { _floors = value; SetupFloorConsoles(); } }

        public void SetupElevatorController()
        {
            Controller = BuildingFactory.CreateController(this);

        }

        private void SetupFloorConsoles()
        {
            _floors.ForEach(f => { f.Console.Controller = Controller;  f.Console.Floor = f; });
        }


        private void SetupElevatorConsoles()
        {
            _elevators.ForEach(e => { e.Console = BuildingFactory.CreateElevatorConsole(this); });
        }

        
    }
}
