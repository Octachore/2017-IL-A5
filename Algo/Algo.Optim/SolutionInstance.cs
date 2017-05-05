using System.Linq;

namespace Algo.Optim
{
    public abstract class SolutionInstance
    {
        readonly SolutionSpace _space;
        double _cost = -1.0;

        protected SolutionInstance( SolutionSpace space, int[] coord )
        {
            _space = space;
            Coordinates = coord;
        }

        public SolutionSpace Space => _space;

        public SolutionInstance FindBestAround()
        {
            SolutionInstance best = this;

            for (int i = 0; i < Coordinates.Length; i++)
            {
                int x = Coordinates[i];
                int y = _space.Cardinalities[i];

                int idx1 = (x + 1) % y;
                int idx2 = (x - 1 + y) % y;

                Try(idx1, i, ref best);
                Try(idx2, i, ref best);
            }

            return best;
        }

        private void Try(int idx, int i, ref SolutionInstance best)
        {
            int[] coord = Coordinates.ToArray();
            coord[i] = idx;

            SolutionInstance @try = _space.CreateSolutionInstance(coord);

            if (@try.Cost < best.Cost) best = @try;
        }

        public int[] Coordinates { get; }

        public double Cost => _cost >= 0 ? _cost : (_cost = ComputeCost());

        double ComputeCost()
        {
            double c = DoComputeCost();
            if (_space.BestSolution == null || c < _space.BestSolution.Cost)
            {
                _space.BestSolution = this;
            }
            if (_space.WorstSolution == null || c > _space.WorstSolution.Cost)
            {
                _space.WorstSolution = this;
            }
            return c;
        }

        protected abstract double DoComputeCost();
    }
}
