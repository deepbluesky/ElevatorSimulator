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
                string whatToDo = ReadData("Select E for Elevator F for Floor Q for Quit ");
                if(whatToDo.ToUpper()[0] == 'F')
                {
                    int floor = Convert.ToInt32(whatToDo.ToUpper().Replace("F", ""));

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
