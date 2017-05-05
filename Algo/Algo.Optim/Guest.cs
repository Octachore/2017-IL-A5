using System.Collections.Generic;

namespace Algo.Optim
{
    public class Guest
    {
        public string Name { get; set; }

        public Airport Location { get; set; }

        public List<SimpleFlight> ArrivalFlights { get; } = new List<SimpleFlight>();

        public List<SimpleFlight> DepartureFlights { get; } = new List<SimpleFlight>();
    }
}
