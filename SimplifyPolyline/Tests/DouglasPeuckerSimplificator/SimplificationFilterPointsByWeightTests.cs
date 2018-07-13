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

        [SetUp]
        public void Setup() {
            this.filter = new SimplificationFilterPointsByWeight();
        }        
        [Test]
        public void Filter() {
            IList<WeightedItem> weightedItems = new List<WeightedItem>();
            IEnumerable<MapItem> actual = this.filter.Filter(weightedItems, new double[] {  }, 100);
            ComparisonHelper.CheckTotalPointsCount(0, actual);

            MapPath mapPath1 = new MapPath();
            MapPathSegment segment1 = Utils.CreateSegment(4);
            mapPath1.Segments.Add(segment1);
            IList<IList<double>> weights1 = new List<IList<double>> { new double[] { 1, 2 } };
            weightedItems = new List<WeightedItem> { new WeightedItem(mapPath1, weights1) };

            actual = this.filter.Filter(weightedItems, new double[] { 1, 2 }, 100);
            ComparisonHelper.CheckTotalPointsCount(4, actual);
            actual = this.filter.Filter(weightedItems, new double[] { 1, 2 }, 0);
            ComparisonHelper.CheckTotalPointsCount(2, actual);

            mapPath1.Segments.Add(segment1);
            weights1.Add(new double[] { 1, 2 });
            actual = this.filter.Filter(weightedItems, new double[] { 1, 1, 2, 2 }, 49);
            ComparisonHelper.CheckTotalPointsCount(6, actual);

            MapPathSegment segment2 = Utils.CreateSegment(5);
            MapPath mapPath2 = new MapPath();
            mapPath2.Segments.Add(segment2);
            IList<IList<double>> weights2 = new List<IList<double>> { new double[] { 1, 2, 3 } };
            weightedItems.Add(new WeightedItem(mapPath2, weights2));
            actual = this.filter.Filter(weightedItems, new double[] { 1, 1, 1, 2, 2, 2, 3 }, 50);
            ComparisonHelper.CheckTotalPointsCount(10, actual);
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
