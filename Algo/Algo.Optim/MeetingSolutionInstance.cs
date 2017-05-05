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
            double cost = 0;
            for (int i = 0; i < Coordinates.Length; i += 2)
            {
                Guest guest = _meeting.Guests[i / 2];
                SimpleFlight arrivalFlight = guest.ArrivalFlights[Coordinates[i]];
                SimpleFlight departureFlight = guest.DepartureFlights[Coordinates[i + 1]];
                cost += CalculateCost(arrivalFlight, departureFlight);
            }
            return cost;
        }

        private static double CalculateCost(SimpleFlight arrivalFlight, SimpleFlight departureFlight)
        {
            return arrivalFlight.Price + departureFlight.Price;
        }

        private double Cost(params Config[] configs)
        {

            foreach (Config config in configs)
            {
                int totalWaitingMinutes = (_meeting.MaxArrivalDate - config.ArrivalFlight.ArrivalTime).Minutes
                                            + (config.DepartureFlight.DepartureTime - _meeting.MinDepartureDate).Minutes;
                cost += config.ArrivalFlight.Price
                        + config.DepartureFlight.Price
                        + GuestMinuteCost(totalWaitingMinutes, config.Guest.MinuteRateCost);

            }
        }

        public double Cardinality() => Meeting.Guests.Select(g => g.ArrivalFlights.Count * g.DepartureFlights.Count).Aggregate(1.0, (acc, cur) => acc * cur);

        public double GuestMinuteCost(double minutes, double rate) => rate * Math.Exp(minutes / 60);

    }
}
