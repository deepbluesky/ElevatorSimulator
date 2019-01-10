using ElevatorSimulator.Commands;
using ElevatorSimulator.Elevators;
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
                        FloorNumber = -(basements - basement),
                        Console = new ElevatorFloorConsole()
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

            int id = 0;
            building.Floors.ForEach(floor =>
            {
                floor.Id = id++;
                floor.Lower = building.Floors.Find(x => x.FloorNumber == floor.FloorNumber - 1) ?? BuildingFactory.NoFloor;
                floor.Upper = building.Floors.Find(x => x.FloorNumber == floor.FloorNumber + 1) ?? BuildingFactory.NoFloor;

            });

            id = 1;
            if (basements + floors > 0 && elevators > 0)
            {
                building.Elevators = Enumerable.Range(0, elevators)
                    .Select(elevator => new Elevator { Name = $"E{id++}" }).ToList<IElevator>();
                building.Elevators.ForEach(elevator => elevator.CurrentFloor = groundFloor);
            }

            building.SetupElevatorController();

            building.Floors.ForEach(floor => floor.Console = new ElevatorFloorConsole {Controller = building.Controller, Floor = floor });

            return building;
        }

        private static Floor _NoFloor = new Floor { FloorNumber = Int32.MinValue };
        public static Floor NoFloor => _NoFloor;

        public static IElevatorController CreateController(Building building)
        {
            return new ElevatorController(building);
        }

        public static IElevatorConsole CreateElevatorConsole(Building building, IElevator elevator)
        {


            IElevatorConsole console = new ElevatorConsole();
            console.Controller = building.Controller;
            elevator.Console = console;
            elevator.Console.FloorButtons = building.Floors
                .Select(floor => new GoToCommand(elevator, floor.FloorNumber))
                .ToList<ICommand<int>>();

            elevator.StopRequests.AddRange(
                building.Floors.Select(floor => new StopRequest { FloorNumber = floor.FloorNumber })
            );


            return console;
        }
    }
}
