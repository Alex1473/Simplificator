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
        public IList<double> CalculateWeights(IList<CoordPoint> points) {
            if (points.Count < 3)
                return new double[] { };

            IList<double> weights = CalculateWeightsCore(points);
            for (int i = 0; i < weights.Count; ++i)
                weights[i] = Math.Sqrt(weights[i]) * 0.65;
            double[] weightsWithoutInfinity = new double[weights.Count - 2];
            Array.Copy(weights.ToArray(), 1, weightsWithoutInfinity, 0, weights.Count - 2);
            return weightsWithoutInfinity;
        }

        public IList<double> CalculateWeightsCore(IList<CoordPoint> points) {
            int[] previousPointIndex = new int[points.Count];
            int[] nextPointIndex = new int[points.Count];
            double[] weights = new double[points.Count];
            double maxValue = double.NegativeInfinity;

            TriangleSquareCalculator squareCalculator = new TriangleSquareCalculator();
            for (int i = 0; i < points.Count; ++i) { 
                if (i == 0 || i == points.Count - 1) 
                    weights[i] = double.PositiveInfinity;
                else 
                    weights[i] = squareCalculator.CalculateSquare(points[i - 1], points[i], points[i + 1]);
                previousPointIndex[i] = i - 1;
                nextPointIndex[i] = i + 1;
            }

            SortedList<WeightedPointIndex, int> sortedList = new SortedList<WeightedPointIndex, int>();
            for (int i = 0; i < points.Count; ++i)
                sortedList.Add(new WeightedPointIndex(i, weights[i]), i);

            while(sortedList.Count > 0) {
                int minWeightPointIndex = sortedList[sortedList.Keys[0]];
                sortedList.RemoveAt(0);
                if (weights[minWeightPointIndex] == double.PositiveInfinity)
                    break;

                if (weights[minWeightPointIndex] < maxValue)
                    weights[minWeightPointIndex] = maxValue;
                else
                    maxValue = weights[minWeightPointIndex];

                int previousIndex = previousPointIndex[minWeightPointIndex];
                int nextIndex = nextPointIndex[minWeightPointIndex];

                if (previousIndex > 0) {
                    double triangleSquare = squareCalculator.CalculateSquare(points[previousPointIndex[previousIndex]], points[previousIndex], points[nextIndex]);
                    weights[previousIndex] = triangleSquare;
                    int x = sortedList.IndexOfValue(previousIndex);
                    sortedList.RemoveAt(sortedList.IndexOfValue(previousIndex));
                    sortedList.Add(new WeightedPointIndex(previousIndex, triangleSquare), previousIndex);
                }

                if (nextIndex < points.Count - 1) {
                    double triangleSquare = squareCalculator.CalculateSquare(points[previousIndex], points[nextIndex], points[nextPointIndex[nextIndex]]);
                    weights[nextIndex] = triangleSquare;
                    int x = sortedList.IndexOfValue(nextIndex);
                    sortedList.RemoveAt(sortedList.IndexOfValue(nextIndex));
                    sortedList.Add(new WeightedPointIndex(nextIndex, triangleSquare), nextIndex);
                }
                previousPointIndex[nextIndex] = previousIndex;
                nextPointIndex[previousIndex] = nextIndex;
            }


            //while (heap.size() > 0) {
            //    c = heap.pop(); // Remove the point with the least effective area.
            //    val = kk[c];
            //    if (val === Infinity) {
            //        break;
            //    }
            //    if (val < maxVal) {
            //        // don't assign current point a lesser value than the last removed vertex
            //        kk[c] = maxVal;
            //    } else {
            //        maxVal = val;
            //    }

            //    // Recompute effective area of neighbors of the removed point.
            //    b = prevArr[c];
            //    d = nextArr[c];
            //    if (b > 0) {
            //        val = calc(prevArr[b], b, d, xx, yy, zz);
            //        heap.updateValue(b, val);
            //    }
            //    if (d < arcLen - 1) {
            //        val = calc(b, d, nextArr[d], xx, yy, zz);
            //        heap.updateValue(d, val);
            //    }
            //    nextArr[b] = d;
            //    prevArr[d] = b;
            //}



            return weights;
        }
    }
}
