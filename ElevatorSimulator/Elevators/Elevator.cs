using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ElevatorSimulator.Elevators;

namespace ElevatorSimulator
{
    public class Elevator : IObservable<IElevator>, IElevator
    {

        public Floor CurrentFloor { get; set; }
        private Floor TargetFloor { get; set; }
        public String Name { get; set; }
        public IElevatorConsole Console { get; set; }

        internal IElevatorState State { get; set; }


        internal IElevatorState GoingUpState = new ElevatorGoingUpState();
        internal IElevatorState GoingDownState = new ElevatorGoingDownState();
        internal IElevatorState DoorOpenState = new ElevatorOpenDoorState();
        internal IElevatorState DoorClosedState = new ElevatorCloseDoorState();
        internal IElevatorState StopState = new ElevatorStopState();

        public List<StopRequest> StopRequests { get; set; }
        

        private object _lock = new object();

        public Elevator()
        {
            State = DoorClosedState; 
            StopRequests = new List<StopRequest>();
            TargetFloor = BuildingFactory.NoFloor;
            GoingUpState.Elevator = GoingDownState.Elevator = DoorOpenState.Elevator = DoorClosedState.Elevator = StopState.Elevator = this;
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


        public void RequestElevator(FloorRequest req)
        {
            var stopRequest = StopRequests.Find(sr => sr.FloorNumber == req.To);
            if (IsGoingDown()) stopRequest.DownRequest = true;
            if (IsGoingUp()) stopRequest.UpRequest = true;


            if (TargetFloor.FloorNumber == CurrentFloor.FloorNumber)
            {
                Land();
                return;
            }

            if (IsIdle())
            {
                TargetFloor = GetFloor(req, CurrentFloor);
                if (req.IsGoingDown) GoDown();
                if (req.IsGoingUp) GoUp();
                return;
            }


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
        public bool IsIdle() => TargetFloor == BuildingFactory.NoFloor || TargetFloor.FloorNumber == CurrentFloor.FloorNumber || State == DoorClosedState;

        private void Land()
        {
            Stop();
            OpenDoor();
        }

        public void GoDownImpl()
        {
            if (TargetFloor == BuildingFactory.NoFloor) return;
            if (!CurrentFloor.Lower.IsNoFloor)
            { 
                lock (_lock)
                    CurrentFloor = CurrentFloor.Lower;

                NotifyObservers();

                StopRequest stopRequest = StopRequests.Find(sr => sr.FloorNumber == CurrentFloor.FloorNumber);
                if (stopRequest.DownRequest)
                {
                    stopRequest.DownRequest = false;
                    Land();
                }

                if (TargetFloor == CurrentFloor)
                {
                    Land();
                    return;
                }
                
                Thread.Sleep(100);
                GoDown();
            }
        }

        public void GoUpImpl()
        {
            if (TargetFloor == BuildingFactory.NoFloor) return;

            if (!CurrentFloor.Upper.IsNoFloor)
            {
                lock (_lock)
                    CurrentFloor = CurrentFloor.Upper;

                NotifyObservers();
                StopRequest stopRequest = StopRequests.Find(sr => sr.FloorNumber == CurrentFloor.FloorNumber);
                if (stopRequest.UpRequest)
                {
                    stopRequest.UpRequest = false;
                    Land();
                } 

                if (TargetFloor == CurrentFloor)
                {
                    Land();
                    return;
                }

                

                Thread.Sleep(100);
                GoUp();
            }
        }

        public void OpenDoorImpl()
        {            
        }

        public void CloseDoorImpl()
        {
        }

        public void StopImpl()
        {
        }


        private List<IObserver<IElevator>> _observers = new List<IObserver<IElevator>>();
        public IDisposable Subscribe(IObserver<IElevator> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            observer.OnNext(this);

            return new Unsubscriber<IElevator>(_observers, observer);
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
