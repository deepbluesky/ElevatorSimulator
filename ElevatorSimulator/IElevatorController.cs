using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator
{
    public interface IElevatorController
    {
        void RequestElevator(int from, int to, bool? up=false, bool? down=false);
    }
}
