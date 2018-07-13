using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Map;
using DevExpress.XtraMap;
using NUnit.Framework;

namespace SimplifyPolyline.Tests
{
    public static class ComparisonHelper {
        public static void AssertArrays(IList<double> expected, IList<double> actual, double accuracy) {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; ++i)
                Assert.AreEqual(expected[i], actual[i], accuracy);
        }
        
        public static void AssertArrays(IList<double> expected, IList<double> actual) {
            AssertArrays(expected, actual, 0);
        }

        public static void AssertWeightedItems(IList<WeightedItem> expected, IList<WeightedItem> actual) {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; ++i) {
                Assert.AreEqual(expected[i].Weights, actual[i].Weights);
                Assert.AreSame(expected[i].Item, actual[i].Item);
            }
        }

        public static void CheckTotalPointsCount(int expectedCount, IEnumerable<MapItem> items) {
            int pointsCount = 0;
            foreach (ISupportCoordPoints item in items)
                pointsCount += item.Points.Count;
            Assert.AreEqual(expectedCount, pointsCount);
        }
    }
}
