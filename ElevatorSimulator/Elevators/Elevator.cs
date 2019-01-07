using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ElevatorSimulator.Elevators;

namespace ElevatorSimulator
{
    public class Elevator : IObservable<Elevator>, IElevator
    {

        public Floor CurrentFloor { get; set; }
        public Floor TargetFloor { get; set; }
        public IElevatorConsole Console { get; set; }

        internal IElevatorState State { get; set; }


        internal IElevatorState GoingUpState = new ElevatorGoingUpState();
        internal IElevatorState GoingDownState = new ElevatorGoingDownState();
        internal IElevatorState DoorOpenState = new ElevatorOpenDoorState();
        internal IElevatorState DoorClosedState = new ElevatorCloseDoorState();
        internal IElevatorState StopState = new ElevatorStopState();

        private object _lock = new object();
        public Elevator()
        {
            State = DoorClosedState;
            Reset();
        }


        public void Reset(bool stop = true)
        {
            TargetFloor = BuildingFactory.NoFloor;
            if (stop) Stop();
        }
        public void OpenDoor()
        {
            State.OpenDoor();
        }
        public void CloseDoor()
        {
            State.CloseDoor();
        }
        public void GoUp()
        {
            State.GoUp();
        }
        public void GoDown()
        {

            State.GoDown();
        }

        public void Stop()
        {
            State.Stop();
        }


        public void SetTarget(FloorRequest req)
        {
            if (IsIdle()) TargetFloor = GetFloor(req, CurrentFloor);
            //elevator is going down and the current target is greater than 'To', 
            //new target is 'To'
            if (IsGoingDown() && TargetFloor.FloorNumber > req.To)
                TargetFloor = GetFloor(req, CurrentFloor);

            //elevator is going up and the current target is smaller than 'To', 
            //new target is 'To'
            if (IsGoingUp() && TargetFloor.FloorNumber < req.To)
                TargetFloor = GetFloor(req, CurrentFloor);
        }

        private Floor GetFloor(FloorRequest req, Floor floor)
        {
            if (floor.FloorNumber == req.To) return floor;

            if (floor.FloorNumber > req.To)
                return GetFloor(req, floor.Lower);
            else
                return GetFloor(req, floor.Upper);
        }

        public bool IsGoingUp() => GoingUpState == State;
        public bool IsGoingDown() => GoingDownState == State;
        public bool IsIdle() => TargetFloor == BuildingFactory.NoFloor;

        private void Arrived()
        {
            Stop();
            OpenDoor();
            Reset(false);
        }

        public void GoDownImpl()
        {
            if (TargetFloor == BuildingFactory.NoFloor) return;
            if (!CurrentFloor.Lower.IsNoFloor)
            {
                if (TargetFloor == CurrentFloor)
                {
                    Arrived();
                    return;
                }

                lock (_lock)
                    CurrentFloor = CurrentFloor.Lower;

                NotifyObservers();

                Thread.Sleep(100);
                GoDown();
            }
        }

        public void GoUpImpl()
        {
            if (TargetFloor == BuildingFactory.NoFloor) return;

            if (!CurrentFloor.Upper.IsNoFloor)
            {
                if (TargetFloor == CurrentFloor)
                {
                    Arrived();
                    return;
                }

                lock (_lock)
                    CurrentFloor = CurrentFloor.Upper;

                NotifyObservers();

                Thread.Sleep(100);
                GoUp();
            }
        }

        private List<IObserver<Elevator>> _observers = new List<IObserver<Elevator>>();
        public IDisposable Subscribe(IObserver<Elevator> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            observer.OnNext(this);

            return new Unsubscriber<Elevator>(_observers, observer);
        }

        public void NotifyObservers()
        {
            _observers?.ForEach(observer => observer.OnNext(this));
        }
    }


    internal class Unsubscriber<T> : IDisposable
    {
        private List<IObserver<T>> _observers;
        private IObserver<T> _observer;

        internal Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
