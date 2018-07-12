using DevExpress.Map;
using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline
{
    public class MapItemsSimplificator {
        public IEnumerable<MapItem> Simplify(IEnumerable<MapItem> items, double percent) {
            double[] weights = CalculateWeights(items);
            double minSuitableWeight = FindMinSuitableWeight(weights, percent);
            return FilterItems(items, minSuitableWeight);
            
        }

        internal double[] CalculateWeights(IEnumerable<MapItem> items) {
            List<double> weights = new List<double>();
            foreach (MapItem item in items)
                if (item is MapPath)
                    CalculateWeights(item as MapPath, weights);
            return weights
                .OrderByDescending(z => z)
                .ToArray();
        }

        internal void CalculateWeights(MapPath item, List<double> resultWeights) {
            DouglasPeuckerSimplyfier douglasPeuckerSimplyfier = new DouglasPeuckerSimplyfier();
            foreach(MapPathSegment segment in item.Segments) {
                resultWeights.AddRange(douglasPeuckerSimplyfier.CalculateWeights(segment.Points.ToArray()).Where(z => z != double.PositiveInfinity));
            } 
        }

        internal double FindMinSuitableWeight(double[] weights, double procent) {
            if (procent == 0)
                return double.PositiveInfinity;
            if (procent == 100)
                return 0;
            if (weights.Length == 0)
                return 0;
            return weights[(int)Math.Floor(weights.Length * procent / 100)];
        }

        internal IEnumerable<MapItem> FilterItems(IEnumerable<MapItem> items, double minSuitableWeight) {
            List<MapItem> filtedItems = new List<MapItem>();
            foreach (MapItem item in items)
                if (item is MapPath)
                    filtedItems.Add(FilterItem(item as MapPath, minSuitableWeight));
            return filtedItems;
        }

        internal MapPath FilterItem(MapPath item, double minSuitableWeight) {
            MapPath filtredPath = new MapPath();
            filtredPath.Segments.BeginUpdate();
            try {
                foreach (MapPathSegment segment in item.Segments)
                    filtredPath.Segments.Add(FilterSegment(segment, minSuitableWeight));
            } finally {
                filtredPath.Segments.EndUpdate();
            }
            return filtredPath;
        }

        internal MapPathSegment FilterSegment(MapPathSegment segment,double minSuitableWeight) {
            DouglasPeuckerSimplyfier douglasPeuckerSimplyfier = new DouglasPeuckerSimplyfier();

            CoordPoint[] points = segment.Points.ToArray();
            IList<double> weights = douglasPeuckerSimplyfier.CalculateWeights(points);

            MapPathSegment filtedSegment = new MapPathSegment();
            for (int i = 0; i < points.Length; ++i)
                if (weights[i] >= minSuitableWeight)
                    filtedSegment.Points.Add(points[i]);

            return filtedSegment;
        }
    }
}
