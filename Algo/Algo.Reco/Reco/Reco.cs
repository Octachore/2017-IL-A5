namespace Algo
{
    public class Reco
    {
        public Movie Movie { get; }

        public double Weight { get; }

        public Reco(Movie movie, double weight)
        {
            Movie = movie;
            Weight = weight;
        }

        public override string ToString() => $"{Movie.Title} — {Weight:#.##}";
    }
}