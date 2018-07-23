using DevExpress.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimplifyPolyline.PolygonsSpliter;
using DevExpress.XtraMap;
using SimplifyPolyline.Utils;

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

    public class SimplificationBySegmentWeightedCalculatorTests {
        SimplificationBySegmentWeightedCalculator calculator;

        [SetUp]
        public void Setup() {
            this.calculator = new SimplificationBySegmentWeightedCalculator(new FakeWeightsCalculator());
        }

        [Test]

        public void Process() {
            List<MapItem> items = new List<MapItem>();
            MapPath mapPath1 = new MapPath();
            items.Add(mapPath1);
            MapPathSegment segment1 = DifferentUtils.CreateSegment(4);
            mapPath1.Segments.Add(segment1);
            this.calculator.Process(items);

            IList<IList<double>> weights1 = new List<IList<double>> { new double[] { 1, 2 } };
            IList<WeightedItem> expectedWeightedItems = new List<WeightedItem> { new WeightedItem(mapPath1, weights1) };
            Assert.AreEqual(new double[] { 1, 2 }, this.calculator.Weights);
            ComparisonHelper.AssertWeightedItems(expectedWeightedItems, this.calculator.WeightedItems);

            mapPath1.Segments.Add(segment1);
            this.calculator.Process(items);
            weights1.Add(new double[] { 1, 2 });
            Assert.AreEqual(new double[] { 1, 2 }, this.calculator.Weights);
            ComparisonHelper.AssertWeightedItems(expectedWeightedItems, this.calculator.WeightedItems);

            MapPathSegment segment2 = DifferentUtils.CreateSegment(6);
            MapPath mapPath2 = new MapPath();
            mapPath2.Segments.Add(segment2);
            items.Add(mapPath2);
            this.calculator.Process(items);
            IList<IList<double>> weights2 = new List<IList<double>>();
            weights2.Add(new double[] { 1, 2, double.PositiveInfinity, 1 });
            expectedWeightedItems.Add(new WeightedItem(mapPath2, weights2));
            Assert.AreEqual(new double[] { 1, 1, 2 }, this.calculator.Weights);
            ComparisonHelper.AssertWeightedItems(expectedWeightedItems, this.calculator.WeightedItems);
        }

        [Test]
        public void ProcessWorksWithoutData() {
            List<MapItem> items = new List<MapItem>();
            this.calculator.Process(items);
            Assert.AreEqual(0, this.calculator.Weights.Count);
            Assert.AreEqual(0, this.calculator.WeightedItems.Count);

            MapPath mapPath1 = new MapPath();
            items.Add(mapPath1);
            this.calculator.Process(items);
            Assert.AreEqual(0, this.calculator.Weights.Count);
            Assert.AreEqual(1, this.calculator.WeightedItems.Count);
        }
    }
}
