using ElevatorSimulator.Elevators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ElevatorSimulator
{
    public class ElevatorController : IObserver<IElevator>
    {
        #region Observer
        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(IElevator elevator)
        {
            Console.WriteLine($"Elevator is at floor : {elevator.CurrentFloor.Name}");
            //if there are any requests from this floor on this direction
            var matchingRequests = 
                _requestQueue.ToList()
                    .FindAll(req => !req.Completed)
                    .FindAll(req =>
                        elevator.IsGoingUp() && req.IsGoingUp || elevator.IsGoingDown() && req.IsGoingDown)
                    .FindAll(req => req.From == elevator.CurrentFloor.FloorNumber);

            if(matchingRequests.Count > 0)
            {
                elevator.Stop();
                elevator.OpenDoor();
                //set all matching requests are completed.
                matchingRequests.ForEach(x => x.Completed = true);
            }

        }

        public virtual void Subscribe(Elevator provider)
        {
            provider.Subscribe(this);
        }
        #endregion

        private Building _building;
        public ElevatorController(Building building)
        {
            _building = building;
            _building.Elevators?.ForEach(elevator => Subscribe(elevator));
            Thread thread = new Thread(Process);
        }

        private void Process(object obj)
        {
            while (_isRunning)
            {
                if (_requestQueue.Count == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }

                //if the request is Completed, purge them
                while (_requestQueue.Count > 0 && _requestQueue.Peek().Completed)
                {
                    _requestQueue.Dequeue();
                }

                HandleRequest(_requestQueue.Dequeue());
            }
        }

        private void HandleRequest(FloorRequest floorRequest)
        {
            Elevator elevator = GetElevator(floorRequest);

            if (elevator != null)
            {
                elevator.SetTarget(floorRequest);
            }
            else
            {
                //no elevators are avaiable at the moment, put the request back in queue.
                RequestElevator(floorRequest);
            }
        }

        private Elevator GetElevator(FloorRequest floorRequest)
        {
            //are there any idle elevators on this floor
            var idleElevator = _building.Elevators.Find(
                x => x.CurrentFloor.FloorNumber == floorRequest.From && x.IsIdle()
                );

            if (idleElevator != null) return idleElevator;

            //get all elevators that are moving in this direction 
            //and are at requested floor
            var sameDirectionalElevators = _building.Elevators.FindAll(x =>
                   (
                       x.IsGoingDown() && floorRequest.IsGoingDown
                       ||
                       x.IsGoingUp() && floorRequest.IsGoingUp
                   ) && x.CurrentFloor.FloorNumber == floorRequest.From
                );

            //there are elevators travelling in this direction
            if (sameDirectionalElevators.Count > 0)
            {
                //probably we can do some optimization here (like open the least weighing elevator
                // , but we need to capture weight of the elevator including the load)
                return sameDirectionalElevators[0];
            }

            //if there are any idle elevators, send one of them
            idleElevator = _building.Elevators.Find(x => x.IsIdle());
            if (idleElevator != null) return idleElevator;

            //none of the elevators are ready now, lets wait for next round
            return null;
        }

        private Queue<FloorRequest> _requestQueue = new Queue<FloorRequest>();

        public void RequestElevator(int from, int to)
        {
            FloorRequest request = new FloorRequest { From = from, To = to };
            RequestElevator(request);


        }

        public void RequestElevator(FloorRequest request)
        {
            //if the request is already present in the queue, ignore it
            if (!_requestQueue.Any(x => x.From == request.From && x.To == request.To))
                _requestQueue.Enqueue(request);
        }

        private volatile bool _isRunning = true;
        public void Shutdown()
        {
            _isRunning = false;
        }

    }
}