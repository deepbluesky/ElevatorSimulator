using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ElevatorSimulator;

namespace EuroFinsTest
{
    class ElevatorMovements
    {
        private Building _building;

        public ElevatorMovements()
        {
            _building = BuildingFactory.Create(2, 10, 3);
        }

         

        [Test]
        public void TestMoveDown()
        {
            _building.Floors.Find(x => x.FloorNumber == -2).Console.GoUp();

            Console.ReadLine();
             
        }
    }
}
