using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraMap;
using NUnit.Framework;

namespace SimplifyPolyline.Tests
{
    [TestFixture]
    public class MapItemsSimplificatorTests {
        MapItemsSimplificator mapItemsSimplificator;
        [SetUp]
        public void Setup() {
            this.mapItemsSimplificator = new MapItemsSimplificator();
        }

        [Test]
        public void CalculateWeights() {
            List<MapItem> items = new List<MapItem>();

            double[] weights = this.mapItemsSimplificator.CalculateWeights(items);
            Assert.AreEqual(new double[] { }, weights);

            items.Add(new MapPath());
            this.mapItemsSimplificator.CalculateWeights(items);
            Assert.AreEqual(new double[] { }, weights);

            items.Clear();
            MapPath mapPath1 = new MapPath();
            MapPathSegment segment1 = new MapPathSegment();
            segment1.Points.AddRange(new GeoPoint[] {
                new GeoPoint(1, 0),
                new GeoPoint(2, 0),
                new GeoPoint(3, 0),
                new GeoPoint(1, 0)
            });

            mapPath1.Segments.Add(segment1);
            items.Add(mapPath1);
            weights = this.mapItemsSimplificator.CalculateWeights(items);
            Assert.AreEqual(2, weights.Length);
            for (int i = 0; i < weights.Length - 1; ++i)
                Assert.GreaterOrEqual(weights[i], weights[i + 1]);

            mapPath1.Segments.Add(segment1);
            weights = this.mapItemsSimplificator.CalculateWeights(items);
            Assert.AreEqual(4, weights.Length);
            weights = weights.ToArray();
            for (int i = 0; i < weights.Length - 1; ++i)
                Assert.GreaterOrEqual(weights[i], weights[i + 1]);
            
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
            weights = this.mapItemsSimplificator.CalculateWeights(items);
            Assert.AreEqual(7, weights.Length);
            weights = weights.ToArray();
            for (int i = 0; i < weights.Length - 1; ++i)
                Assert.GreaterOrEqual(weights[i], weights[i + 1]);
        }

        [Test]
        public void FindMinSuitableWeight() {
            double[] weights = new double[] { };
            Assert.AreEqual(double.PositiveInfinity, this.mapItemsSimplificator.FindMinSuitableWeight(weights, 0));
            Assert.AreEqual(0, this.mapItemsSimplificator.FindMinSuitableWeight(weights, 100));


            weights = new double[] { 4, 3, 2, 1 };
            Assert.AreEqual(double.PositiveInfinity, this.mapItemsSimplificator.FindMinSuitableWeight(weights, 0));
            Assert.AreEqual(0, this.mapItemsSimplificator.FindMinSuitableWeight(weights, 100));
            Assert.AreEqual(2, this.mapItemsSimplificator.FindMinSuitableWeight(weights, 50));
        } 

    }
}
