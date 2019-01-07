using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator
{
    public class Floor
    {
        public string Name { get; set; }
        public int FloorNumber { get; set; }
        public ElevatorFloorConsole Console {get; set;}
        public Floor Upper { get; set; }
        public Floor Lower { get; set; }

        public bool IsNoFloor => FloorNumber == Int32.MinValue;
        public int Id;


    }
}
