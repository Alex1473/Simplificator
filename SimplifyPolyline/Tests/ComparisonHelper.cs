using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SimplifyPolyline.Tests
{
    public static class ComparisonHelper {
        public static void AssertArrays(double[] expected, double[] actual, double accuracy) {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; ++i)
                Assert.AreEqual(expected[i], actual[i], accuracy);
        }
        
        public static void AssertArrays(double[] expected, double[] actual) {
            AssertArrays(expected, actual, 0);
        }

        public static void AssertWeightedItems(IList<WeightedItem> expected, IList<WeightedItem> actual) {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; ++i) {
                Assert.AreEqual(expected[i].Weights, actual[i].Weights);
                Assert.AreSame(expected[i].Item, actual[i].Item);
            }
        }
    }
}
