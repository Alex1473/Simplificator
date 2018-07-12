using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline {
    public class WeightsCalculator {

        List<WeightedItem> weightedItems;
        List<double> weights;

        public IList<WeightedItem> WeightedItems {
            get {
                return this.weightedItems;
            }
        }

        public IList<double> Weights {
            get {
                return this.weights;
            }
        }

        void Process(MapPath mapPath) {
            DouglasPeuckerSimplyfier douglasPeuckerSimplyfier = new DouglasPeuckerSimplyfier();
            List<IList<double>> segmentsWeights = new List<IList<double>>();

            foreach (MapPathSegment segment in mapPath.Segments) {
                IList<double> calculatedWeights = douglasPeuckerSimplyfier.CalculateWeights(segment.Points);
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
                    Process(mapPath);
            }
        }
    }
}
