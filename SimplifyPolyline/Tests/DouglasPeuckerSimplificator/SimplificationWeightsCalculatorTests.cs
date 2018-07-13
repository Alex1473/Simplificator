using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Map;
using DevExpress.XtraMap;
using NUnit.Framework;

namespace SimplifyPolyline.Tests {
    public class FakeWeightsCalculator : IWeightsCalculator {
        public IList<double> CalculateWeights(IList<CoordPoint> points) {
            if (points.Count < 2)
                return new double[] { };
            double[] weights = new double[points.Count - 2];
            for (int i = 0; i < weights.Length; ++i)
                weights[i] = i + 1;
            return weights;
        }
    }
    [TestFixture]
    public class SimplificationWeightsCalculatorTests {
        SimplificationWeightsCalculator simplificationWeightsCalculator;

        [SetUp]
        public void Setup() {
            this.simplificationWeightsCalculator = new SimplificationWeightsCalculator(new FakeWeightsCalculator());
        }

        [Test]
        public void Process() {
            List<MapItem> items = new List<MapItem>();
            MapPath mapPath1 = new MapPath();
            items.Add(mapPath1);
            MapPathSegment segment1 = Utils.CreateSegment(4);
            mapPath1.Segments.Add(segment1);
            this.simplificationWeightsCalculator.Process(items);

            IList<IList<double>> weights1 = new List<IList<double>> { new double[] { 1, 2 } };
            IList<WeightedItem> expectedWeightedItems = new List<WeightedItem> { new WeightedItem(mapPath1, weights1) };
            Assert.AreEqual(new double[] { 1, 2 }, this.simplificationWeightsCalculator.Weights);
            ComparisonHelper.AssertWeightedItems(expectedWeightedItems, this.simplificationWeightsCalculator.WeightedItems);
            
            mapPath1.Segments.Add(segment1);
            this.simplificationWeightsCalculator.Process(items);
            weights1.Add(new double[] { 1, 2 });
            Assert.AreEqual(new double[] { 1, 1, 2, 2 }, this.simplificationWeightsCalculator.Weights);
            ComparisonHelper.AssertWeightedItems(expectedWeightedItems, this.simplificationWeightsCalculator.WeightedItems);
            
            MapPathSegment segment2 = Utils.CreateSegment(5);
            MapPath mapPath2 = new MapPath();
            mapPath2.Segments.Add(segment2);
            items.Add(mapPath2);
            this.simplificationWeightsCalculator.Process(items);
            IList<IList<double>> weights2 = new List<IList<double>>();
            weights2.Add(new double[] { 1, 2, 3 });
            expectedWeightedItems.Add(new WeightedItem(mapPath2, weights2));
            Assert.AreEqual(new double[] { 1, 1, 1, 2, 2, 2, 3 }, this.simplificationWeightsCalculator.Weights);
            ComparisonHelper.AssertWeightedItems(expectedWeightedItems, this.simplificationWeightsCalculator.WeightedItems);
        }

        [Test]
        public void ProcessWorksWithoutData() {
            List<MapItem> items = new List<MapItem>();
            this.simplificationWeightsCalculator.Process(items);
            Assert.AreEqual(0, this.simplificationWeightsCalculator.Weights.Count);
            Assert.AreEqual(0, this.simplificationWeightsCalculator.WeightedItems.Count);

            MapPath mapPath1 = new MapPath();
            items.Add(mapPath1);
            this.simplificationWeightsCalculator.Process(items);
            Assert.AreEqual(0, this.simplificationWeightsCalculator.Weights.Count);
            Assert.AreEqual(1, this.simplificationWeightsCalculator.WeightedItems.Count);
        }

        [Test]
        public void WeightedItemsAndWeights() {
            Assert.IsNull(this.simplificationWeightsCalculator.WeightedItems);
            Assert.IsNull(this.simplificationWeightsCalculator.Weights);

            List<MapItem> items = new List<MapItem>();
            this.simplificationWeightsCalculator.Process(items);
            Assert.IsNotNull(this.simplificationWeightsCalculator.WeightedItems);
            Assert.IsNotNull(this.simplificationWeightsCalculator.Weights);
        }
    }
}
