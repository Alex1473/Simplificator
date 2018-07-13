using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;
using NUnit.Framework;

namespace SimplifyPolyline.Tests {
    [TestFixture]
    public class SimplificationFilterPointsByWeightTests {
        SimplificationFilterPointsByWeight filter;

        MapPathSegment CreateSegment(int pointsCount) {
            MapPathSegment segment = new MapPathSegment();
            for (int i = 0; i < pointsCount; ++i)
                segment.Points.Add(new GeoPoint(0, 0));
            return segment;
        }
        double CalculateTotalPointsCount(IEnumerable<MapItem> items) {
            int pointsCount = 0;
            foreach(ISupportCoordPoints item in items) 
                pointsCount += item.Points.Count;
            return pointsCount;
        }

        [SetUp]
        public void Setup() {
            this.filter = new SimplificationFilterPointsByWeight();
        }        
        [Test]
        public void Filter() {
            IList<WeightedItem> weightedItems = new List<WeightedItem>();
            IEnumerable<MapItem> actual = this.filter.Filter(weightedItems, new double[] {  }, 100);
            Assert.AreEqual(0, CalculateTotalPointsCount(actual));

            MapPath mapPath1 = new MapPath();
            MapPathSegment segment1 = CreateSegment(4);
            mapPath1.Segments.Add(segment1);
            IList<IList<double>> weights1 = new List<IList<double>> { new double[] { 1, 2 } };
            weightedItems = new List<WeightedItem> { new WeightedItem(mapPath1, weights1) };

            actual = this.filter.Filter(weightedItems, new double[] { 1, 2 }, 100);
            Assert.AreEqual(4, CalculateTotalPointsCount(actual));
            actual = this.filter.Filter(weightedItems, new double[] { 1, 2 }, 0);
            Assert.AreEqual(2, CalculateTotalPointsCount(actual));
            
            mapPath1.Segments.Add(segment1);
            weights1.Add(new double[] { 1, 2 });
            actual = this.filter.Filter(weightedItems, new double[] { 1, 1, 2, 2 }, 49);
            Assert.AreEqual(6, CalculateTotalPointsCount(actual));
            
            MapPathSegment segment2 = CreateSegment(5);
            MapPath mapPath2 = new MapPath();
            mapPath2.Segments.Add(segment2);
            IList<IList<double>> weights2 = new List<IList<double>> { new double[] { 1, 2, 3 } };
            weightedItems.Add(new WeightedItem(mapPath2, weights2));
            actual = this.filter.Filter(weightedItems, new double[] { 1, 1, 1, 2, 2, 2, 3 }, 50);
            Assert.AreEqual(10, CalculateTotalPointsCount(actual));
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
