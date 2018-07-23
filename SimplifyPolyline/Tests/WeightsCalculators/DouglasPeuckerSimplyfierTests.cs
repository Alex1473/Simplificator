using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraMap;
using NUnit.Framework;

namespace SimplifyPolyline.Tests
{
    [TestFixture]
    public class DouglasPeuckerSimplyfierTests {
        DouglasPeuckerWeightsCalculator douglasPeuckerSimplyfier;

        [SetUp]
        public void Setup() {
            this.douglasPeuckerSimplyfier = new DouglasPeuckerWeightsCalculator();          
        }
        [Test]
        public void CalculateWeights() {
            IList<double> weights = this.douglasPeuckerSimplyfier.CalculateWeights(new GeoPoint[] { });
            Assert.AreEqual(0, weights.Count);

            ComparisonHelper.AssertArrays(new double[] { }, 
                this.douglasPeuckerSimplyfier.CalculateWeights(new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1) }));

            GeoPoint[] input = new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(0, 1), new GeoPoint(1, 0), new GeoPoint(0, 0) };
            ComparisonHelper.AssertArrays(new double[] { 1,  1 }, this.douglasPeuckerSimplyfier.CalculateWeights(input).ToArray(), 1e-6);

            input = new GeoPoint[] {
                new GeoPoint(-1, -3),
                new GeoPoint(-3, 1),
                new GeoPoint(-1, 3),
                new GeoPoint(4, 3),
                new GeoPoint(5, 1),
                new GeoPoint(3, -2),
                new GeoPoint(-1, -3)
            };

            ComparisonHelper.AssertArrays(new double[] { 4.0971801, 1.3736056, 4.0971801, 1.37281294, 2.4327007 }, this.douglasPeuckerSimplyfier.CalculateWeights(input).ToArray(), 1e-6);

            input = new GeoPoint[] {
                new GeoPoint(-2, 1),
                new GeoPoint(-4, 2),
                new GeoPoint(-2, 3),
                new GeoPoint(20, 0),
                new GeoPoint(-2, -3),
                new GeoPoint(-3, -2),
                new GeoPoint(-1, -1),
                new GeoPoint(-2, 1)
            };

            ComparisonHelper.AssertArrays(new double[] { 2.2360679, 1.1626367, 3.9958741,
                3.9958741, 1, 1 }, this.douglasPeuckerSimplyfier.CalculateWeights(input).ToArray(), 1e-6);
        }
    }
}
