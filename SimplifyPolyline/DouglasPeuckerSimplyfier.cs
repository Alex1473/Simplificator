using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;

namespace SimplifyPolyline {
    public class DouglasPeuckerSimplyfier : ISimplifyPolyline {
        public double[] CalculateWeights(CoordPoint[] points) {
            if (points.Length == 0)
                return new double[] { };
            if (points.Length == 1)
                return new double[] { double.PositiveInfinity };

            return CalculateWeightsCore(points);
        }

        double[] CalculateWeightsCore(CoordPoint[] points) {
            double[] weights = new double[points.Length];
            weights[0] = double.PositiveInfinity;
            weights[points.Length - 1] = double.PositiveInfinity;
            if (points.Length > 2)
                CalculateWeightsRecursive(points, 0, points.Length - 1, double.PositiveInfinity, 1, weights);
            return weights;
        }
        double CalculateWeightsRecursive(CoordPoint[] points, int firstIndex, int lastIndex, double previousSquareDistance, int depth, double[] weights) {
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


            if ((depth == 1) && (firstPoint == lastPoint))
                maxDistanceSquare = Math.Max(distanceSquareLeft, distanceSquareRight);
            weights[maxIndex] = Math.Sqrt(maxDistanceSquare);
            return maxDistanceSquare;
        }


        double CalculateMinSuitableWeight(double[] weights, double percentOfPoints) {
            int suitablePointsQuantity = (int)Math.Ceiling((weights.Length - 2) * percentOfPoints   / 100) + 2;
            weights = weights.OrderByDescending(z => z)
                             .ToArray();
            while ((suitablePointsQuantity < weights.Length) && (weights[suitablePointsQuantity] == weights[suitablePointsQuantity - 1]))
                ++suitablePointsQuantity;
            
            

            return weights[suitablePointsQuantity - 1];

        }
        


    internal CoordPoint[] FilterPointsByWeight(CoordPoint[] points, double[] weights, double percentOfMaxWeight) {
            if (Math.Abs(50 - percentOfMaxWeight) > 50)
                throw new ArgumentException("Percent of max weight must be between 0 and 100");

            double minSuitableWeight = CalculateMinSuitableWeight(weights, percentOfMaxWeight);

            List<CoordPoint> suitablePoints = new List<CoordPoint>();
            for (int i = 0; i < points.Length; ++i)
                if (weights[i] >= minSuitableWeight)
                    suitablePoints.Add(points[i]);
            return suitablePoints.ToArray();
        }




        public CoordPoint[] Simplify(CoordPoint[] points, double percentAccuracy) {
            if (Math.Abs(50 - percentAccuracy) > 50)
                throw new ArgumentException("Percent accuracy of max weight must be between 0 and 100");

            return FilterPointsByWeight(points, CalculateWeights(points), percentAccuracy);
        }

        
    }
}
