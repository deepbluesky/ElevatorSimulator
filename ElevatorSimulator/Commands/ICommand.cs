using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulator
{
    public interface ICommand<T>
    {
        void Execute();
        T Data {get; set;}
    }
}
