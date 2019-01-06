using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorSimulator
{
    public class BuildingFactory
    {
        public static Building Create(int basements, int floors, int elevators = 0)
        {
            Building building = new Building(basements, floors, elevators);
            if (basements > 0)
                building.Floors = Enumerable.Range(0, basements)
                    .Select(basement => new Floor
                    {
                        Name = $"B{basements - basement}",
                        FloorNumber = -(basements - basement)
                    }).ToList();

            Floor groundFloor = new Floor { Name = "Ground Floor", FloorNumber = 0 };
            building.Floors.Add(groundFloor);

            building.Floors.AddRange(
                Enumerable.Range(1, floors)
                        .Select(floor => new Floor
                        {
                            Name = $"F{floor}",
                            FloorNumber = floor
                        })
            );

            //null object pattern
            Floor noFloor = new Floor { FloorNumber = Int32.MinValue };

            building.Floors.ForEach(floor =>
            {
                floor.Lower = building.Floors.Find(x => x.FloorNumber == floor.FloorNumber - 1) ?? noFloor;
                floor.Upper = building.Floors.Find(x => x.FloorNumber == floor.FloorNumber + 1) ?? noFloor;
            });

            if (basements + floors > 0 && elevators > 0)
            {
                building.Elevators = Enumerable.Range(0, elevators + 1)
                    .Select( elevator => new Elevator()).ToList();
                building.Elevators.ForEach(elevator => elevator.CurrentFloor = groundFloor);
            }

            return building;
        }
    }
}
