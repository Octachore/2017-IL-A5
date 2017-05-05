using System;

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

        public SolutionInstance MonteCarlo(int nbTry)
        {
            SolutionInstance best = null;

            while (--nbTry >= 0)
            {
                SolutionInstance current = GetRandomInstance();
                SolutionInstance bestAround = current.FindBestAround();

                while (current != bestAround)
                {
                    current = bestAround;
                    bestAround = current.FindBestAround();
                }

                if (current.Cost < best.Cost) best = current;
            }

            return best;
        }
        
        internal abstract SolutionInstance CreateSolutionInstance(int[] coord);
    }
}
