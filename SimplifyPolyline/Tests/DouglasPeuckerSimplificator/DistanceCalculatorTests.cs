using System;
using System.Linq;
using DevExpress.XtraMap;
using NUnit.Framework;

namespace SimplifyPolyline.Tests
{
    [TestFixture]
    public class DistanceCalculatorTests {
        DistanceCalculator distanceCalculator;

        [SetUp]
        public void Setup() {
            this.distanceCalculator = new DistanceCalculator();
        }

        [Test]
        public void CalculateSquareDistance() {
            Assert.AreEqual(0, distanceCalculator.CalculateSquareDistance(new GeoPoint(0, 0), new GeoPoint(0, 0)));
            Assert.AreEqual(2, distanceCalculator.CalculateSquareDistance(new GeoPoint(0, 0), new GeoPoint(1, 1)));
        }
        [Test]
        public void CalculateSquareDistanceFromPointToSegment() {
            GeoPoint firstSegmentPoint = new GeoPoint(0, 0);
            GeoPoint secondSegmentPoint = new GeoPoint(2, 0);

            Assert.AreEqual(2, distanceCalculator.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(-1, 1)));
            Assert.AreEqual(1, distanceCalculator.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(1, 1)));
            Assert.AreEqual(2, distanceCalculator.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(3, 1)));
            Assert.AreEqual(1, distanceCalculator.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(-1, 0)));
            Assert.AreEqual(0, distanceCalculator.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(1, 0)));
            Assert.AreEqual(1, distanceCalculator.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(3, 0)));
            Assert.AreEqual(0, distanceCalculator.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(1, 0)));
            Assert.AreEqual(0, distanceCalculator.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(0, 0)));
        }
    }
}
