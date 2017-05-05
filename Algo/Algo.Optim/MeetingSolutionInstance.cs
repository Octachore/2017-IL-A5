using System;
using System.Linq;

namespace Algo.Optim
{
    public class MeetingSolutionInstance : SolutionInstance
    {
        private readonly Meeting _meeting;

        public MeetingSolutionInstance(Meeting meeting, int[] coord) : base(meeting, coord)
        {
            _meeting = meeting;
        }

        protected override double DoComputeCost()
        {
            var guests = _meeting.Guests.Select((g, i) => new
            {
                Guest = g,
                Arrival = ArrivalFor(i),
                Departure = DepartureFor(i),
                Index = i
            });

            DateTime lastArrivalTime = guests.Max(g => g.Arrival.ArrivalTime);
            DateTime firstDepartureTime = guests.Min(g => g.Departure.DepartureTime);

            return guests.Sum(g => CalculateIndividualCost(g.Guest, g.Arrival, g.Departure, lastArrivalTime, firstDepartureTime));
        }

        private SimpleFlight ArrivalFor(int i) => _meeting.Guests[i].ArrivalFlights[Coordinates[i * 2]];
        private SimpleFlight DepartureFor(int i) => _meeting.Guests[i].DepartureFlights[Coordinates[i * 2 + 1]];

        private double CalculateIndividualCost(Guest guest, SimpleFlight arrivalFlight, SimpleFlight departureFlight, DateTime lastArrivalTime, DateTime firstDepartureTime)
        {
            int totalWaitingMinutes = (lastArrivalTime - arrivalFlight.ArrivalTime).Minutes
                                        + (departureFlight.DepartureTime - firstDepartureTime).Minutes;

            return arrivalFlight.Price + departureFlight.Price + GuestMinuteCost(totalWaitingMinutes, _meeting.WaitingMinuteCost);
        }

        public double GuestMinuteCost(double minutes, double baseMinuteCost, int offset = 10) => baseMinuteCost * Math.Exp((minutes - offset) / 60);
    }
}
