using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;
using NUnit.Framework;
using SimplifyPolyline.PolygonsSpliter; 

namespace SimplifyPolyline.Tests {
    [TestFixture]
    public class ArcIndexTests {
        ArcIndex arcIndex;
        
        [Test]
        public void AddArc() {
            this.arcIndex = new ArcIndex(new CoordPoint[] { });
            PackagedArcs actual = arcIndex.GetVertexData();
            IList<int> expectedLength = new List<int>();
            List<CoordPoint> expectedPoints = new List<CoordPoint>();
            Assert.AreEqual(expectedLength, actual.ArcsLength);
            Assert.AreEqual(expectedPoints, actual.ArcsPoints);

            this.arcIndex.AddArc(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1) });
            expectedLength.Add(2);
            expectedPoints.AddRange(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1) });
            actual = arcIndex.GetVertexData();
            Assert.AreEqual(expectedLength, actual.ArcsLength);
            Assert.AreEqual(expectedPoints, actual.ArcsPoints);

            this.arcIndex.AddArc(new CoordPoint[] { new GeoPoint(3, 3), new GeoPoint(4, 4) });
            expectedLength.Add(2);
            expectedPoints.AddRange(new CoordPoint[] { new GeoPoint(3, 3), new GeoPoint(4, 4) });
            actual = arcIndex.GetVertexData();
            Assert.AreEqual(expectedLength, actual.ArcsLength);
            Assert.AreEqual(expectedPoints, actual.ArcsPoints);
        }
        
    }
}
