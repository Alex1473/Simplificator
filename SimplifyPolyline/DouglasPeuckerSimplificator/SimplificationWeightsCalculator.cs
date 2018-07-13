using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraMap;

namespace SimplifyPolyline {
    public class SimplificationWeightsCalculator : ISimplificationWeightsCalculator {
        readonly IWeightsCalculator weightsCalculator;
        List<WeightedItem> weightedItems;
        List<double> weights;

        public IList<WeightedItem> WeightedItems { get { return this.weightedItems;}}
        public IList<double> Weights { get { return this.weights;}}

        public SimplificationWeightsCalculator(IWeightsCalculator weightsCalculator) {
            this.weightsCalculator = weightsCalculator;
        }

        void ProcessCore(MapPath mapPath) {
            IList<IList<double>> segmentsWeights = new List<IList<double>>();
            foreach (MapPathSegment segment in mapPath.Segments) {
                IList<double> calculatedWeights = this.weightsCalculator.CalculateWeights(segment.Points);
                segmentsWeights.Add(calculatedWeights);
                this.weights.AddRange(calculatedWeights);
            }
            this.weightedItems.Add(new WeightedItem(mapPath, segmentsWeights));
        }
        public void Process(IEnumerable<MapItem> items) {
            this.weightedItems = new List<WeightedItem>();
            this.weights = new List<double>();
            foreach(MapItem item in items) {
                MapPath mapPath = item as MapPath;
                if (mapPath != null)
                    ProcessCore(mapPath);
            }
            this.weights.Sort();
        }
    }
}
