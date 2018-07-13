using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraMap;

namespace SimplifyPolyline {
    public class SimplificationFilterPointsByWeight : ISimplificationFilter {
        MapItem FilterItem(WeightedItem weightedItem, double minSuitableWeight) {
            MapPath filtredPath = new MapPath();
            filtredPath.Segments.BeginUpdate();
            try {
                for (int i = 0; i < weightedItem.Item.Segments.Count; ++i)
                    filtredPath.Segments.Add(FilterSegment(weightedItem.Item.Segments[i], weightedItem.Weights[i], minSuitableWeight));
            } finally {
                filtredPath.Segments.EndUpdate();
            }
            return filtredPath;
        }
        internal double FindMinSuitableWeight(IList<double> weights, double percent) {
            if (percent == 0)
                return double.PositiveInfinity;
            if (percent == 100 || weights.Count == 0)
                return 0;
            return weights[weights.Count - (int)Math.Floor(weights.Count * percent / 100) - 1];
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
                filtedSegment.Points.Add(segment.Points[segment.Points.Count - 1]);
            } finally {
                filtedSegment.Points.EndUpdate();
            }
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
