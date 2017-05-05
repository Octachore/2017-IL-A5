using System;
using System.Collections.Generic;

namespace Algo.Optim
{
    public class Meeting
    {
        public Meeting(Airport location)
        {
            Location = location;
            Guests = new List<Guest>
            {
                new Guest() { Name = "Adolf", Location = Airport.FindByCode("BER") },
                new Guest() { Name = "Adeline", Location = Airport.FindByCode("CDG") },
                new Guest() { Name = "Marcel", Location = Airport.FindByCode("MRS") },
                new Guest() { Name = "Léon", Location = Airport.FindByCode("LYS") },
                new Guest() { Name = "Peter", Location = Airport.FindByCode("MAN") },
                new Guest() { Name = "Jose", Location = Airport.FindByCode("BIO") },
                new Guest() { Name = "Donald", Location = Airport.FindByCode("JFK") },
                new Guest() { Name = "Youssef", Location = Airport.FindByCode("TUN") },
                new Guest() { Name = "Mario", Location = Airport.FindByCode("MXP") },
            };

            MaxArrivalDate = new DateTime(2010, 7, 27, 17, 0, 0);
            MinDepartureDate = new DateTime(2010, 8, 3, 15, 0, 0);
        }

        public List<Guest> Guests { get; }

        public DateTime MaxArrivalDate { get; }

        public DateTime MinDepartureDate { get; }

        public Airport Location { get; private set; }
    }
}
