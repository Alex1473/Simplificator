using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;
using NUnit.Framework;
using SimplifyPolyline;

namespace SimplifyPolyline.Tests {

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
            this.simplificationWeightsCalculator.Process(items);
            Assert.AreEqual(0, this.simplificationWeightsCalculator.Weights.Count());
            Assert.AreEqual(0, this.simplificationWeightsCalculator.WeightedItems.Count());

            MapPath mapPath1 = new MapPath();
            items.Add(mapPath1);
            this.simplificationWeightsCalculator.Process(items);
            Assert.AreEqual(0, this.simplificationWeightsCalculator.Weights.Count());
            Assert.AreEqual(1, this.simplificationWeightsCalculator.WeightedItems.Count());

            MapPathSegment segment1 = new MapPathSegment();
            segment1.Points.AddRange(new GeoPoint[] {
                new GeoPoint(1, 0),
                new GeoPoint(2, 0),
                new GeoPoint(3, 0),
                new GeoPoint(1, 0)
            });
            mapPath1.Segments.Add(segment1);
            this.simplificationWeightsCalculator.Process(items);
            Assert.AreEqual(new double[] { 1, 2 }, this.simplificationWeightsCalculator.Weights);
            Assert.AreEqual(new double[] { 1, 2 }, this.simplificationWeightsCalculator.WeightedItems[0].Weights[0]);

            mapPath1.Segments.Add(segment1);
            this.simplificationWeightsCalculator.Process(items);
            Assert.AreEqual(new double[] { 1, 1, 2, 2 }, this.simplificationWeightsCalculator.Weights);
            Assert.AreEqual(new double[] { 1, 2 }, this.simplificationWeightsCalculator.WeightedItems[0].Weights[0]);
            Assert.AreEqual(new double[] { 1, 2 }, this.simplificationWeightsCalculator.WeightedItems[0].Weights[1]);


            MapPathSegment segment2 = new MapPathSegment();
            segment2.Points.AddRange(new GeoPoint[] {
                new GeoPoint(1, 0),
                new GeoPoint(2, 0),
                new GeoPoint(3, 0),
                new GeoPoint(4, 0),
                new GeoPoint(1, 0)
            });
            MapPath mapPath2 = new MapPath();
            mapPath2.Segments.Add(segment2);
            items.Add(mapPath2);
            this.simplificationWeightsCalculator.Process(items);
            Assert.AreEqual(new double[] { 1, 1, 1, 2, 2, 2, 3 }, this.simplificationWeightsCalculator.Weights);
            Assert.AreEqual(new double[] { 1, 2 }, this.simplificationWeightsCalculator.WeightedItems[0].Weights[0]);
            Assert.AreEqual(new double[] { 1, 2 }, this.simplificationWeightsCalculator.WeightedItems[0].Weights[1]);
            Assert.AreEqual(new double[] { 1, 2, 3 }, this.simplificationWeightsCalculator.WeightedItems[1].Weights[0]);




        }




    }



    public class FakeWeightsCalculator : IWeightsCalculator {
        IList<double> IWeightsCalculator.CalculateWeights(IList<CoordPoint> points) {
            if (points.Count < 2)
                return new double[] { };

            double[] weights = new double[points.Count - 2];
            for (int i = 0; i < weights.Length; ++i)
                weights[i] = i + 1;
            return weights;
        }
    }
}
