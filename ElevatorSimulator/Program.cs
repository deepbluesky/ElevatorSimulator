using ElevatorSimulator.Elevators;
using System;

namespace ElevatorSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var building = BuildingFactory.Create(2, 10, 3);

            bool stop = false;

            while (!stop)
            {
                string whatToDo = ReadData("Select E for Elevator F for Floor Q for Quit! (FU - floor  up)  (E1 - Elevator 1) ");
                if (whatToDo.ToUpper()[0] == 'F')
                {
                    int floorNbr = Convert.ToInt32(ReadData("Which Floor:"));

                    Floor floor = building.Floors.Find(x => x.FloorNumber == floorNbr);

                    if (floor == null) continue;

                    if (whatToDo.ToUpper()[1] == 'U') floor.Console.GoUp();
                    if (whatToDo.ToUpper()[1] == 'D') floor.Console.GoDown();
                }

                if (whatToDo.ToUpper()[0] == 'E')
                {
                    int elNumber = Convert.ToInt32("" + whatToDo.ToUpper()[1]);

                    IElevator elevator = building.Elevators.Find(x => x.Name == $"E{elNumber}");

                    if (elevator == null) continue;

                    string operation = ReadData("Select O top open door , C - to close Door , F for navigate to a floor ) ");

                    if (operation.ToUpper()[0] == 'C') elevator.CloseDoor();
                    if (operation.ToUpper()[0] == 'O') elevator.OpenDoor();

                    if (operation.ToUpper()[0] == 'F')
                    {
                        int floorNbr = Convert.ToInt32(ReadData("Which Floor:"));

                        elevator.Console.FloorButtons.Find(fb => fb.Data == floorNbr)?.Execute();
                    }
                }


            }

        }

        static string ReadData(string prompt)
        {
            Console.Write($"\n{prompt}: ");
            return Console.ReadLine();
        }
    }
}
