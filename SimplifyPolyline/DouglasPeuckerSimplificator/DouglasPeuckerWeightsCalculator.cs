using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;

namespace SimplifyPolyline {
    public class DouglasPeuckerWeightsCalculator : IWeightsCalculator {
        IList<double> CalculateWeightsCore(IList<CoordPoint> points) {
            double[] weights = new double[points.Count];
            weights[0] = double.PositiveInfinity;
            weights[points.Count - 1] = double.PositiveInfinity;
            if (points.Count > 2)
                CalculateWeightsRecursive(points, 0, points.Count - 1, double.PositiveInfinity, 1, weights);
            double[] weightsWithoutInfinity = new double[weights.Length - 2];
            Array.Copy(weights, 1, weightsWithoutInfinity, 0, weights.Length - 2);
            return weightsWithoutInfinity;
        }
        double CalculateWeightsRecursive(IList<CoordPoint> points, int firstIndex, int lastIndex, double previousSquareDistance, int depth, double[] weights) {
            CoordPoint firstPoint = points[firstIndex];
            CoordPoint lastPoint = points[lastIndex];
            double maxDistanceSquare = -1;
            int maxIndex = 0;
            double distanceSquareLeft = 0, distanceSquareRight = 0;

            DistanceCalculator distanceCalculater = new DistanceCalculator();
            for (int i = firstIndex + 1; i < lastIndex; ++i) {
                double distanceSquare = distanceCalculater.CalculateSquareDistanceFromPointToSegment(firstPoint, lastPoint, points[i]);
                if (distanceSquare >= maxDistanceSquare) {
                    maxDistanceSquare = distanceSquare;
                    maxIndex = i;
                }
            }

            if (previousSquareDistance < maxDistanceSquare)
                maxDistanceSquare = previousSquareDistance;

            if (maxIndex - firstIndex > 1)
                distanceSquareLeft = CalculateWeightsRecursive(points, firstIndex, maxIndex, maxDistanceSquare, depth + 1, weights);
            if (lastIndex - maxIndex > 1)
                distanceSquareRight = CalculateWeightsRecursive(points, maxIndex, lastIndex, maxDistanceSquare, depth + 1, weights);


            if (depth == 1 && firstPoint == lastPoint)
                maxDistanceSquare = Math.Max(distanceSquareLeft, distanceSquareRight);
            weights[maxIndex] = Math.Sqrt(maxDistanceSquare);
            return maxDistanceSquare;
        }
        public IList<double> CalculateWeights(IList<CoordPoint> points) {
            if (points.Count == 0)
                return new double[] { };
            if (points.Count == 1)
                return new double[] { double.PositiveInfinity };
            return CalculateWeightsCore(points);
        }
    }
}
