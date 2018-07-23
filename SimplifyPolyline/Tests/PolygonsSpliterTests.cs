using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplifyPolyline.PolygonsSpliter;
using DevExpress.Map;
using DevExpress.XtraMap;

namespace SimplifyPolyline.Tests {

    [TestFixture]
    public class PolygonsSpliterTests {

        PolygonsSpliter.PolygonsSpliter polygonsSpliter;

        [SetUp]
        public void Setup() {
            this.polygonsSpliter = new PolygonsSpliter.PolygonsSpliter();
        }

        [Test]
        public void InitializePointChains() {
            Assert.AreEqual(new double[] { }, this.polygonsSpliter.InitializePointChains(new CoordPoint[] { }));
            Assert.AreEqual(new double[] { 0 }, this.polygonsSpliter.InitializePointChains(new CoordPoint[] { new GeoPoint(0, 0) }));
            Assert.AreEqual(new double[] { 0, 1, 2 }, this.polygonsSpliter.InitializePointChains(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2) }));
            Assert.AreEqual(new double[] { 1, 2, 0 }, this.polygonsSpliter.InitializePointChains(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(0, 0), new GeoPoint(0, 0) }));
            Assert.AreEqual(new double[] { 3, 5, 2, 4, 0, 1 }, this.polygonsSpliter.InitializePointChains(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1),
                new GeoPoint(2, 2), new GeoPoint(0, 0), new GeoPoint(0, 0), new GeoPoint(1, 1)}));
        }

        [Test]
        public void InitializePathIds() {
            Assert.AreEqual(new int[] { }, this.polygonsSpliter.InitializePathIds(new int[] { }));
            Assert.AreEqual(new int[] { 0 }, this.polygonsSpliter.InitializePathIds(new int[] { 1 }));
            Assert.AreEqual(new int[] { 0, 1, 1, 1, 2, 2 }, this.polygonsSpliter.InitializePathIds(new int[] { 1, 3, 2 }));
        }

        [Test]
        public void CalculateNextPointIndex() {
            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 1 }, new CoordPoint[] { new GeoPoint(0, 0) }));
            Assert.AreEqual(-1, this.polygonsSpliter.CalculateNextPointIndex(0));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 2 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1) }));
            Assert.AreEqual(1, this.polygonsSpliter.CalculateNextPointIndex(0));
            Assert.AreEqual(-1, this.polygonsSpliter.CalculateNextPointIndex(1));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 4 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2), new GeoPoint(0, 0) }));
            Assert.AreEqual(1, this.polygonsSpliter.CalculateNextPointIndex(3));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 1, 4, 1 }, new CoordPoint[] { new GeoPoint(0, 0),  new GeoPoint(1, 1), new GeoPoint(2, 2),
                new GeoPoint(3, 3), new GeoPoint(1, 1), new GeoPoint(4, 4)}));
            Assert.AreEqual(2, this.polygonsSpliter.CalculateNextPointIndex(4));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 1, 3, 1 }, new CoordPoint[] { new GeoPoint(0, 0),  new GeoPoint(1, 1), new GeoPoint(2, 2),
                new GeoPoint(3, 3), new GeoPoint(4, 4)}));
            Assert.AreEqual(-1, this.polygonsSpliter.CalculateNextPointIndex(3));
        }

        [Test]
        public void CalculatePreviousPointIndex() {
            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 1 }, new CoordPoint[] { new GeoPoint(0, 0) }));
            Assert.AreEqual(-1, this.polygonsSpliter.CalculatePreviousPointIndex(0));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 2 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1) }));
            Assert.AreEqual(-1, this.polygonsSpliter.CalculatePreviousPointIndex(0));
            Assert.AreEqual(0, this.polygonsSpliter.CalculatePreviousPointIndex(1));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 4 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2), new GeoPoint(0, 0)}));
            Assert.AreEqual(2, this.polygonsSpliter.CalculatePreviousPointIndex(0));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 1, 4, 1 }, new CoordPoint[] { new GeoPoint(0, 0),  new GeoPoint(1, 1), new GeoPoint(2, 2),
                new GeoPoint(3, 3), new GeoPoint(1, 1), new GeoPoint(4, 4)}));
            Assert.AreEqual(3, this.polygonsSpliter.CalculatePreviousPointIndex(1));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 1, 3, 1 }, new CoordPoint[] { new GeoPoint(0, 0),  new GeoPoint(1, 1), new GeoPoint(2, 2),
                new GeoPoint(3, 3), new GeoPoint(4, 4)}));
            Assert.AreEqual(-1, this.polygonsSpliter.CalculatePreviousPointIndex(1));
        }

        [Test]
        public void PointIsArcEndpoint() {
            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 1 }, new CoordPoint[] { new GeoPoint(0, 0) }));
            Assert.True(this.polygonsSpliter.PointIsArcEndpoint(0));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 3 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2)}));
            Assert.True(this.polygonsSpliter.PointIsArcEndpoint(0));
            Assert.False(this.polygonsSpliter.PointIsArcEndpoint(1));
            Assert.True(this.polygonsSpliter.PointIsArcEndpoint(2));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 3, 3 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2),
                new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2) }));
            Assert.False(this.polygonsSpliter.PointIsArcEndpoint(1));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 3, 2 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2),
                new GeoPoint(1, 1), new GeoPoint(2, 2) }));
            Assert.True(this.polygonsSpliter.PointIsArcEndpoint(1));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 3, 3, 3 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2),
                new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(3, 3),  new GeoPoint(2, 2), new GeoPoint(1, 1), new GeoPoint(3, 3)}));
            Assert.True(this.polygonsSpliter.PointIsArcEndpoint(1));
        }

        [Test] 
        public void MergeArcParts() {
            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 1 }, new CoordPoint[] { new GeoPoint(0, 0) }));
            Assert.AreEqual(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(0, 0) }, this.polygonsSpliter.MergeArcParts(0, 0, 0, 0));

            this.polygonsSpliter.Split(new PackagedArcs(new int[] { 2, 2 }, new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2),
                new GeoPoint(3, 3) }));
            Assert.AreEqual(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(2, 2), new GeoPoint(3, 3) }, this.polygonsSpliter.MergeArcParts(0, 1, 2, 3));
            Assert.AreEqual(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(1, 1), new GeoPoint(2, 2) }, this.polygonsSpliter.MergeArcParts(0, 1, 1, 2));
        }

    }
}
