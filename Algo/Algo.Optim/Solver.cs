namespace Algo.Optim
{
    public class Solver
    {
        public FlightDatabase Database { get; }

        public Meeting Meeting { get; set; }

        public Solver(string flightDatabasePath, string locationCode)
        {
            Database = new FlightDatabase(flightDatabasePath);
            Meeting = new Meeting(Airport.FindByCode(locationCode));
        }
    }
}
