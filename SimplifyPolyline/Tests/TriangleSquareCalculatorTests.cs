using DevExpress.XtraMap;
using NUnit.Framework;
using SimplifyPolyline.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline.Tests {
    [TestFixture]
    public class TriangleSquareCalculatorTests {
        TriangleSquareCalculator triangleSquareCalculator;
        
        [SetUp]
        public void Setup() {
            this.triangleSquareCalculator = new TriangleSquareCalculator();
        }

        [Test]
        public void CalculateSquare() {

            Assert.AreEqual(0, this.triangleSquareCalculator.CalculateSquare(new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(0, 0)));
            Assert.AreEqual(0.5, this.triangleSquareCalculator.CalculateSquare(new GeoPoint(1, 0), new GeoPoint(0, 0), new GeoPoint(0, 1)));
            Assert.AreEqual(2, this.triangleSquareCalculator.CalculateSquare(new GeoPoint(3, -1), new GeoPoint(0, 0), new GeoPoint(1, 1)));
        }


    }
}
