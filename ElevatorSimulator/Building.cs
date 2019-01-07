using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ElevatorSimulator
{
    public class Building  
    {
        private List<Floor> _floors;

        private List<Elevator> _elevators;

        private ElevatorController _controller;

        public Building(int basements, int floors, int elevators = 0)
        {
            _floors = new List<Floor>();
            _elevators = new List<Elevator>();
        } 

        public List<Elevator> Elevators { get => _elevators; set => _elevators = value; }
        public List<Floor> Floors { get => _floors; set => _floors = value; }

        public void SetupElevatorController()
        {
            _controller = new ElevatorController(this);
        }
    }
}
