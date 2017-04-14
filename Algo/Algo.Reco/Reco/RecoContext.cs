using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using static System.Math;

namespace Algo
{
    public class RecoContext
    {
        public User[] Users { get; private set; }
        public Movie[] Movies { get; private set; }

        public void LoadFrom( string folder )
        {
            Users = User.ReadUsers( Path.Combine( folder, "users.dat" ) );
            Movies = Movie.ReadMovies( Path.Combine( folder, "movies.dat" ) );
            User.ReadRatings( Users, Movies, Path.Combine( folder, "ratings.dat" ) );
        }

        public double DistNorm2( User u1, User u2 )
        {
            var sum = u1.Ratings.Select(mr1 => new
                        {
                            R1 = mr1.Value,
                            R2 = u2.Ratings.GetValueWithDefault(mr1.Key, -1)
                        })
                        .Where(r1r2 => r1r2.R2 >= 0)
                        .Select(r1r2 => r1r2.R1 - r1r2.R2)
                        .Select(delta => delta * delta)
                        .Sum();
            return Sqrt( sum );
        }

        public double Similarity(double distance) => 1 / (1 + distance);

        public static double Pearson(User u1, User u2)
        {
            var a = u1.Ratings.Select(mr1 => new
            {
                R1 = mr1.Value,
                R2 = u2.Ratings.GetValueWithDefault(mr1.Key, -1)
            }).Where(r1r2 => r1r2.R2 >= 0);

            return Pearson(a.Select(i => i.R1).ToList(), a.Select(i => i.R2).ToList());
        }

        public static double Pearson(List<int> x, List<int> y) => Covariance(x, y) / (Deviation(x) * Deviation(y));

        private static double Deviation(List<int> range)
        {
            double mean = ArithmeticMean(range);
            return Sqrt(range.Sum(r => Pow(r - mean, 2)));
        }

        private static double Covariance(List<int> x, List<int> y)
        {
            if (x.Count != y.Count) throw new ArgumentException();

            double meanX = ArithmeticMean(x);
            double meanY = ArithmeticMean(y);
            double cov = 0;
            for (int i = 0; i < x.Count; i++)
            {
                cov += (x[i] - meanX) * (y[i] - meanY);
            }
            return cov;
        }

        private static double ArithmeticMean(List<int> range) => (1.0 / range.Count) * range.Sum();
    }


    public static class DictionaryExtension
    {
        public static TValue GetValueWithDefault<TKey, TValue>( this Dictionary<TKey,TValue> @this, TKey key, TValue def )
        {
            TValue v;
            return @this.TryGetValue(key, out v) ? v : def;
        }
    }
}
