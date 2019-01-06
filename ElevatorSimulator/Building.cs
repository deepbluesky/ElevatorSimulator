using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ElevatorSimulator
{
    public class Building
    {
        private List<Floor> _floors;

        public Building(int basements, int floors)
        {
            
            if (basements > 0)
                _floors = Enumerable.Range(0, basements)
                    .Select(basement => new Floor
                    {
                        Name = $"B{basements - basement}",
                        FloorNumber =   - (basements - basement)
                    }).ToList();

            _floors.Add(new Floor { Name = "Ground Floor", FloorNumber = 0 });

            _floors.AddRange(
                Enumerable.Range(1, floors)
                        .Select(floor => new Floor
                        {
                            Name = $"F{floor}",
                            FloorNumber = floor
                        })
            );

        }

        public List<Floor> Floors { get { return _floors; } }
    }
}
