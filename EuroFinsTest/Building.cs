using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ElevatorSimulator;

namespace EuroFinsTest
{
    class BuildingHarness
    {
        [Test]
        public void TestBuildingConstructor()
        {
            var building = BuildingFactory.Create(2, 10);

            Assert.AreEqual(building.Floors.Count, 13);
        }
    }
}
