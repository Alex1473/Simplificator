using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraMap;
using NUnit.Framework;

namespace SimplifyPolyline.Tests {
    [TestFixture]
    public class SimplificationFilterPointsByWeightTests {

        SimplificationFilterPointsByWeight filter;
        MapPathSegment CreateSegment(int pointsNumber) {
            MapPathSegment segment = new MapPathSegment();
            for (int i = 0; i < pointsNumber; ++i)
                segment.Points.Add(new GeoPoint(0, 0));
            return segment;
        }

        double CountPointsQuantityInMapItems(IEnumerable<MapItem> items) {
            int pointsQuantity = 0;
            foreach(MapItem item in items) {
                MapPath path = item as MapPath;
                foreach (MapPathSegment segment in path.Segments)
                    pointsQuantity += segment.Points.Count;
            }
            return pointsQuantity;
        }

        [SetUp]
        public void Setup() {
            this.filter = new SimplificationFilterPointsByWeight();
        }
        
        [Test]
        public void Filter() {
            IList<WeightedItem> weightedItems = new List<WeightedItem>();
            IEnumerable<MapItem> actual = this.filter.Filter(weightedItems, new double[] {  }, 100);
            Assert.AreEqual(0, CountPointsQuantityInMapItems(actual));

            MapPath mapPath1 = new MapPath();
            MapPathSegment segment1 = CreateSegment(4);
            mapPath1.Segments.Add(segment1);
            IList<IList<double>> weights1 = new List<IList<double>>();
            weights1.Add(new double[] { 1, 2 });
            weightedItems = new List<WeightedItem>();
            weightedItems.Add(new WeightedItem(mapPath1, weights1));

            actual = this.filter.Filter(weightedItems, new double[] { 1, 2 }, 100);
            Assert.AreEqual(4, CountPointsQuantityInMapItems(actual));
            actual = this.filter.Filter(weightedItems, new double[] { 1, 2 }, 0);
            Assert.AreEqual(2, CountPointsQuantityInMapItems(actual));


            mapPath1.Segments.Add(segment1);
            weights1.Add(new double[] { 1, 2 });
            actual = this.filter.Filter(weightedItems, new double[] { 1, 1, 2, 2 }, 49);
            Assert.AreEqual(6, CountPointsQuantityInMapItems(actual));


            MapPathSegment segment2 = CreateSegment(5);
            MapPath mapPath2 = new MapPath();
            mapPath2.Segments.Add(segment2);
            IList<IList<double>> weights2 = new List<IList<double>>();
            weights2.Add(new double[] { 1, 2, 3 });
            weightedItems.Add(new WeightedItem(mapPath2, weights2));
            actual = this.filter.Filter(weightedItems, new double[] { 1, 1, 1, 2, 2, 2, 3 }, 50);
            Assert.AreEqual(10, CountPointsQuantityInMapItems(actual));
        }

        [Test]
        public void FindMinSuitableWeight() {
            Assert.AreEqual(0, this.filter.FindMinSuitableWeight(new double[] { }, 100));
            Assert.AreEqual(double.PositiveInfinity, this.filter.FindMinSuitableWeight(new double[] { }, 0));
            Assert.AreEqual(0, this.filter.FindMinSuitableWeight(new double[] { }, 50));

            double[] weights = new double[] { 1, 2, 3, 4 };
            Assert.AreEqual(0, this.filter.FindMinSuitableWeight(new double[] { }, 100));
            Assert.AreEqual(double.PositiveInfinity, this.filter.FindMinSuitableWeight(new double[] { }, 0));
            Assert.AreEqual(2, this.filter.FindMinSuitableWeight(weights, 50));
            Assert.AreEqual(3, this.filter.FindMinSuitableWeight(weights, 27));
        }

    }
}
