using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using SimplifyPolyline.Utils;

namespace SimplifyPolyline
{
    public class VislalingamEffectiveAreaWeightsCalculator : IWeightsCalculator {
        
        IList<double> CalculateWeightsCore(IList<CoordPoint> points) {
            int[] previousPointIndex = new int[points.Count];
            int[] nextPointIndex = new int[points.Count];
            double maxValue = double.NegativeInfinity;

            double[] weights = InicializeWeights(points);
            InicializeIndices(previousPointIndex, nextPointIndex);
            SortedSet<WeightedIndex> sortedSet = InicializeSortedSet(weights);

            while (sortedSet.Count > 0) {
                WeightedIndex minWeightIndex = sortedSet.Min;
                sortedSet.Remove(minWeightIndex);
                if (minWeightIndex.Weight == double.PositiveInfinity)
                    break;

                if (minWeightIndex.Weight < maxValue)
                    weights[minWeightIndex.Index] = maxValue;
                else
                    maxValue = minWeightIndex.Weight;

                int previousIndex = previousPointIndex[minWeightIndex.Index];
                int nextIndex = nextPointIndex[minWeightIndex.Index];

                if (previousIndex > 0) 
                    UpdatePointWeight(previousIndex, MathUtils.CalculateSquare(points[previousPointIndex[previousIndex]], points[previousIndex], points[nextIndex]),
                        weights, sortedSet);

                if (nextIndex < points.Count - 1) 
                    UpdatePointWeight(nextIndex, MathUtils.CalculateSquare(points[previousIndex], points[nextIndex], points[nextPointIndex[nextIndex]]),
                        weights, sortedSet);
                    
                previousPointIndex[nextIndex] = previousIndex;
                nextPointIndex[previousIndex] = nextIndex;
            }
            return weights;
        }
        void InicializeIndices(int[] previousPointIndex, int[] nextPointIndex) {
            for (int i = 0; i < previousPointIndex.Length; ++i) {
                previousPointIndex[i] = i - 1;
                nextPointIndex[i] = i + 1;
            }
        }

        SortedSet<WeightedIndex> InicializeSortedSet(IList<double> weights) {
            SortedSet<WeightedIndex> sortedSet = new SortedSet<WeightedIndex>();
            for (int i = 0; i < weights.Count; ++i)
                sortedSet.Add(new WeightedIndex(i, weights[i]));
            return sortedSet;
        }

        void UpdatePointWeight(int index, double newWeight, IList<double> weights, SortedSet<WeightedIndex> sortedSet) {
            sortedSet.Remove(new WeightedIndex(index, weights[index]));
            weights[index] = newWeight;
            sortedSet.Add(new WeightedIndex(index, weights[index]));
        }
        internal double[] InicializeWeights(IList<CoordPoint> points) {
            if (points.Count == 0)
                return new double[0] { };

            double[] weights = new double[points.Count];
            weights[0] = double.PositiveInfinity;
            weights[points.Count - 1] = double.PositiveInfinity;
            for (int i = 1; i < points.Count - 1; ++i)
                weights[i] = MathUtils.CalculateSquare(points[i - 1], points[i], points[i + 1]);
            return weights;
        }
        public IList<double> CalculateWeights(IList<CoordPoint> points) {
            if (points.Count == 0)
                return new double[] { };

            IList<double> weights = CalculateWeightsCore(points);
            for (int i = 0; i < weights.Count; ++i)
                weights[i] = Math.Sqrt(weights[i]) * 0.65;
            double[] weightsWithoutInfinity = new double[weights.Count - 2];
            Array.Copy(weights.ToArray(), 1, weightsWithoutInfinity, 0, weights.Count - 2);
            return weightsWithoutInfinity;
        }
    }
}
