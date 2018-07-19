using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;
using SimplifyPolyline.Utils;

namespace SimplifyPolyline.PolygonsSplitor
{
    public class SimplficationBySegmentWeightedCalculator : ISimplificationWeightsCalculator {
        List<WeightedItem> weightedItems;
        List<double> weights;
        IWeightsCalculator weightsCalculator;
        Dictionary<CoordPoint, double> dictionary;

        public SimplficationBySegmentWeightedCalculator(IWeightsCalculator weightsCalculator) {
            this.weightsCalculator = weightsCalculator;
        }

        public IList<WeightedItem> WeightedItems { get { return this.weightedItems; } }
        public IList<double> Weights { get { return this.weights; } }

        public void Process(IEnumerable<MapItem> items) {
            PolygonsPackager polygonsPackager = new PolygonsPackager();
            PackagedPolygons packagedPolygons = polygonsPackager.PackPolygons(items);
            PolygonsSplitor polygonsSplitor = new PolygonsSplitor();
            PackagedPolygons arcs = polygonsSplitor.Split(packagedPolygons.PolygonsPoints, packagedPolygons.PolygonsLength);
            Initialize(arcs.PolygonsPoints, arcs.PolygonsLength);
            this.weightedItems = new List<WeightedItem>();
            
            foreach(MapItem mapItem in items) {
                MapPath mapPath = mapItem as MapPath;
                if (mapPath != null)
                    ProcessCore(mapPath);
            }
            this.weights.Sort();
        }

        void Initialize(IList<CoordPoint> arcsPoints, IList<int> arcsLength) {
            if (arcsPoints.Count == 0)
                return;

            int arcStart = 0;
            this.dictionary = new Dictionary<CoordPoint, double>();
            this.weights = new List<double>();

            for (int i = 0; i < arcsLength.Count; ++i) {
                IList<double> arcWeights = this.weightsCalculator.CalculateWeights(DifferentUtils.GetSubSequance(arcsPoints, arcStart, arcStart + arcsLength[i]));
                this.weights.AddRange(arcWeights);

                if (!this.dictionary.ContainsKey(arcsPoints[arcStart]))
                    this.dictionary.Add(arcsPoints[arcStart], double.PositiveInfinity);
                if (!this.dictionary.ContainsKey(arcsPoints[arcStart + arcsLength[i] - 1]))
                    this.dictionary.Add(arcsPoints[arcStart + arcsLength[i] - 1], double.PositiveInfinity);


                for (int j = arcStart + 1; j < arcStart + arcsLength[i] - 1; ++j)
                    if (!this.dictionary.ContainsKey(arcsPoints[j]))
                        this.dictionary.Add(arcsPoints[j], arcWeights[j - arcStart - 1]);
                arcStart += arcsLength[i];
            }
        }

        void ProcessCore(MapPath mapPath) {
            IList<IList<double>> segmentsWeights = new List<IList<double>>();
            foreach (MapPathSegment segment in mapPath.Segments)
                segmentsWeights.Add(ProcessCoreSegment(segment));
            this.weightedItems.Add(new WeightedItem(mapPath, segmentsWeights));
        }

        IList<double> ProcessCoreSegment(MapPathSegment segment) {
            IList<double> pointsWeight = new List<double>();

            for (int i = 1; i < segment.Points.Count - 1; ++i)
                pointsWeight.Add(this.dictionary[segment.Points[i]]);
            return pointsWeight;
        }




    }
}
