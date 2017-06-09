using System;
using System.Linq;

namespace Algo.Optim
{
    public abstract class SolutionSpace
    {
        readonly Random _random;

        protected SolutionSpace(int seed )
        {
            _random = new Random(seed);
        }

        public SolutionInstance BestSolution { get; internal set; }

        public SolutionInstance WorstSolution { get; internal set; }

        protected void Initialize(int[] cardinalities)
        {
            Cardinalities = cardinalities;
        }

        public int Dimension => Cardinalities.Length;

        public int[] Cardinalities { get; private set; }

        public SolutionInstance GetRandomInstance()
        {
            int[] coord = new int[Dimension];
            for(int i = 0; i < Dimension; ++i )
            {
                coord[i] = _random.Next(Cardinalities[i]);
            }
            return CreateSolutionInstance(coord);
        }

        public void TryRandom(int nbTry)
        {
            while (--nbTry >= 0)
            {
                var c = GetRandomInstance().Cost;
            }
        }

        public SolutionInstance SimulatedAnnealing(int nbTry)
        {
            SolutionInstance s = SimulatedAnnealing();
            for (int i = 1; i < nbTry; i++)
            {
                SolutionInstance sn = SimulatedAnnealing();
                if (sn.Cost < s.Cost) s = sn;
            }
            return s;
        }

        public SolutionInstance SimulatedAnnealing()
        {
            SolutionInstance s = GetRandomInstance();
            double cost = s.Cost;
            double k = 1.0;
            double kmin = 0.000001;
            double alpha = 0.9; // temperature level ratio

            while (k > kmin)
            {
                for (int i = 0; i < 100; i++)
                {
                    var n = s.Neighbors.ToArray();
                    var sn = n[_random.Next(0, n.Length - 1)];
                    if(sn.Cost < s.Cost || Math.Exp((s.Cost - sn.Cost)/k) > _random.Next())
                    {
                        s = sn;
                    }
                }
                k *= alpha;
            }

            return s;
        }

        internal protected abstract SolutionInstance CreateSolutionInstance(int[] coord);
    }
}
