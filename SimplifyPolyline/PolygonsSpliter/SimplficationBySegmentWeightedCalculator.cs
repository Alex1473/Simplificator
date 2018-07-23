using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;
using SimplifyPolyline.Utils;

namespace SimplifyPolyline.PolygonsSpliter {
    public class SimplificationBySegmentWeightedCalculator : ISimplificationWeightsCalculator {
        List<WeightedItem> weightedItems;
        List<double> weights;
        IWeightsCalculator weightsCalculator;
        Dictionary<CoordPoint, double> weightedPoints;

        public SimplificationBySegmentWeightedCalculator(IWeightsCalculator weightsCalculator) {
            this.weightsCalculator = weightsCalculator;
        }

        public IList<WeightedItem> WeightedItems { get { return this.weightedItems; } }
        public IList<double> Weights { get { return this.weights; } }

        void Initialize(PackagedArcs packagedArcs) {
            if (packagedArcs.ArcsPoints.Count == 0) {
                this.weights = new List<double>();
                this.weightedItems = new List<WeightedItem>();
                return;
            }

            int arcStart = 0;
            this.weightedPoints = new Dictionary<CoordPoint, double>();
            this.weights = new List<double>();

            for (int i = 0; i < packagedArcs.ArcsLength.Count; ++i) {
                IList<double> arcWeights = this.weightsCalculator.CalculateWeights(DifferentUtils.GetSubSequence(packagedArcs.ArcsPoints, arcStart, arcStart + packagedArcs.ArcsLength[i]));
                this.weights.AddRange(arcWeights);

                if (!this.weightedPoints.ContainsKey(packagedArcs.ArcsPoints[arcStart]))
                    this.weightedPoints.Add(packagedArcs.ArcsPoints[arcStart], double.PositiveInfinity);
                if (!this.weightedPoints.ContainsKey(packagedArcs.ArcsPoints[arcStart + packagedArcs.ArcsLength[i] - 1]))
                    this.weightedPoints.Add(packagedArcs.ArcsPoints[arcStart + packagedArcs.ArcsLength[i] - 1], double.PositiveInfinity);


                for (int j = arcStart + 1; j < arcStart + packagedArcs.ArcsLength[i] - 1; ++j)
                    if (!this.weightedPoints.ContainsKey(packagedArcs.ArcsPoints[j]))
                        this.weightedPoints.Add(packagedArcs.ArcsPoints[j], arcWeights[j - arcStart - 1]);
                arcStart += packagedArcs.ArcsLength[i];
            }
        }

        WeightedItem ProcessPath(MapPath mapPath) {
            IList<IList<double>> segmentsWeights = new List<IList<double>>();
            foreach (MapPathSegment segment in mapPath.Segments)
                segmentsWeights.Add(ProcessSegment(segment));
            return new WeightedItem(mapPath, segmentsWeights);
        }

        IList<double> ProcessSegment(MapPathSegment segment) {
            IList<double> pointsWeight = new List<double>();
            for (int i = 1; i < segment.Points.Count - 1; ++i)
                pointsWeight.Add(this.weightedPoints[segment.Points[i]]);
            return pointsWeight;
        }

        List<WeightedItem> ProcessItems(IEnumerable<MapItem> items) {
            List<WeightedItem> weightedItems = new List<WeightedItem>();
            foreach (MapItem mapItem in items) {
                MapPath mapPath = mapItem as MapPath;
                if (mapPath != null)
                    weightedItems.Add(ProcessPath(mapPath));
            }
            return weightedItems;
        }

        public void Process(IEnumerable<MapItem> items) {
            PackagedArcs packagedPolygons = new PackagedArcs(items);
            PolygonsSpliter polygonsSpliter = new PolygonsSpliter();
            PackagedArcs arcs = polygonsSpliter.Split(packagedPolygons);
            Initialize(arcs);
            this.weights.Sort();
            this.weightedItems = ProcessItems(items);
        }
    }
}
