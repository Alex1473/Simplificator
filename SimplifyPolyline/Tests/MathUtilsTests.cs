using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraMap;
using NUnit.Framework;
using SimplifyPolyline.Utils;

namespace SimplifyPolyline.Tests {

    [TestFixture]
    public class MathUtilsTests {
        [Test]
        public void CalculateSquare() {
            Assert.AreEqual(0, MathUtils.CalculateSquare(new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(0, 0)));
            Assert.AreEqual(0.5, MathUtils.CalculateSquare(new GeoPoint(1, 0), new GeoPoint(0, 0), new GeoPoint(0, 1)));
            Assert.AreEqual(2, MathUtils.CalculateSquare(new GeoPoint(3, -1), new GeoPoint(0, 0), new GeoPoint(1, 1)));
        }

        [Test]
        public void CalculateSquareDistance() {
            Assert.AreEqual(0, MathUtils.CalculateSquareDistance(new GeoPoint(0, 0), new GeoPoint(0, 0)));
            Assert.AreEqual(2, MathUtils.CalculateSquareDistance(new GeoPoint(0, 0), new GeoPoint(1, 1)));
        }
        [Test]
        public void CalculateSquareDistanceFromPointToSegment() {
            GeoPoint firstSegmentPoint = new GeoPoint(0, 0);
            GeoPoint secondSegmentPoint = new GeoPoint(2, 0);

            Assert.AreEqual(2, MathUtils.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(-1, 1)));
            Assert.AreEqual(1, MathUtils.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(1, 1)));
            Assert.AreEqual(2, MathUtils.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(3, 1)));
            Assert.AreEqual(1, MathUtils.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(-1, 0)));
            Assert.AreEqual(0, MathUtils.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(1, 0)));
            Assert.AreEqual(1, MathUtils.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(3, 0)));
            Assert.AreEqual(0, MathUtils.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(1, 0)));
            Assert.AreEqual(0, MathUtils.CalculateSquareDistanceFromPointToSegment(firstSegmentPoint, secondSegmentPoint, new GeoPoint(0, 0)));
        }
    }
}
