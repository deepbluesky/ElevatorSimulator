using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ElevatorSimulator;

namespace EuroFinsTest
{
    class BuildingHarness
    {
        private Building _building;

        public BuildingHarness()
        {
            _building = BuildingFactory.Create(2, 10, 3);
        }

        [Test]
        public void TestBuildingFactory()
        { 
            Assert.AreEqual(_building.Floors.Count, 13);
        }

        [Test]
        public void TestElevatorInitialPosition()
        {
            Assert.AreEqual(_building.Elevators.Count, 3);
            Assert.IsTrue(_building.Elevators.TrueForAll( x => x.CurrentFloor.FloorNumber == 0));
            Assert.IsTrue(_building.Elevators.TrueForAll(x => x.IsIdle()));
        }
    }
}
