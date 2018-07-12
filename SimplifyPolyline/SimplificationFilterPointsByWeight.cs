using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline {
    public class SimplificationFilterPointsByWeight : ISimplificationFilter {
        internal double FindMinSuitableWeight(IList<double> weights, double procent) {
            if (procent == 0)
                return double.PositiveInfinity;
            if (procent == 100)
                return 0;
            if (weights.Count == 0)
                return 0;
            return weights[weights.Count - (int)Math.Floor(weights.Count * procent / 100) - 1];
        }
        internal MapItem FilterItem(WeightedItem weightedItem, double minSuitableWeight) {
            MapPath filtredPath = new MapPath();
            filtredPath.Segments.BeginUpdate();
            try {
                for(int i = 0; i < weightedItem.Item.Segments.Count; ++i) 
                    filtredPath.Segments.Add(FilterSegment(weightedItem.Item.Segments[i], weightedItem.Weights[i], minSuitableWeight));
            } finally {
                filtredPath.Segments.EndUpdate();
            }
            return filtredPath;
        }
        internal MapPathSegment FilterSegment(MapPathSegment segment, IList<double> weights, double minSuitableWeight) {
            MapPathSegment filtedSegment = new MapPathSegment();
            if (segment.Points.Count == 0)
                return filtedSegment;

            filtedSegment.Points.Add(segment.Points[0]);
            if (segment.Points.Count == 1)
                return filtedSegment;
            filtedSegment.Points.BeginUpdate();
            try {
                for (int i = 1; i < segment.Points.Count - 1; ++i)
                    if (weights[i - 1] >= minSuitableWeight)
                        filtedSegment.Points.Add(segment.Points[i]);
            } finally {
                filtedSegment.Points.EndUpdate();
            }
            filtedSegment.Points.Add(segment.Points[segment.Points.Count - 1]);
            return filtedSegment;
        }
        public IEnumerable<MapItem> Filter(IEnumerable<WeightedItem> weightedItems, IList<double> weights, double percent) {
            double minSuitableWeight = FindMinSuitableWeight(weights, percent);
            List<MapItem> filtedItems = new List<MapItem>();
            foreach (WeightedItem item in weightedItems)
                filtedItems.Add(FilterItem(item, minSuitableWeight));
            return filtedItems;
        }
    }
}
