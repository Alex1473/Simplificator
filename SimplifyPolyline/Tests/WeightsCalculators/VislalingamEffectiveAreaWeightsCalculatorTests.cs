using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;
using NUnit.Framework;

namespace SimplifyPolyline.Tests
{
    [TestFixture]
    public class VislalingamEffectiveAreaWeightsCalculatorTests {
        VislalingamEffectiveAreaWeightsCalculator weightsCalculator; 

        [SetUp]
        public void Setup() {
            this.weightsCalculator = new VislalingamEffectiveAreaWeightsCalculator();
        }

        [Test]
        public void CalculateWeights() {
            Assert.AreEqual(new double[] { }, this.weightsCalculator.CalculateWeights(new CoordPoint[] { }));
            Assert.AreEqual(new double[] { }, this.weightsCalculator.CalculateWeights(new CoordPoint[] { new GeoPoint(0, 1), new GeoPoint(0, 2) }));

            ComparisonHelper.AssertArrays(new double[] { 0.4596194, 0.4596194 }, this.weightsCalculator.CalculateWeights(new CoordPoint[] {
                new GeoPoint(0, 0), new GeoPoint(1, 0), new GeoPoint(0, 1), new GeoPoint(0, 0)}), 1e-6);

            ComparisonHelper.AssertArrays(new double[] { 0.4596194, 0.4596194 }, this.weightsCalculator.CalculateWeights(new CoordPoint[] {
                new GeoPoint(1, 0), new GeoPoint(1, 1), new GeoPoint(0, 1), new GeoPoint(0, 0)}), 1e-6);

            double[] expected = new double[] { 2.6, 1.4534441, 2.6, 1.2160386, 2.0034345};
            GeoPoint[] input = new GeoPoint[] {
                new GeoPoint(-1, -3),
                new GeoPoint(-3, 1),
                new GeoPoint(-1, 3),
                new GeoPoint(4, 3),
                new GeoPoint(5, 1),
                new GeoPoint(3, -2),
                new GeoPoint(-1, -3)
            };

            ComparisonHelper.AssertArrays(expected, this.weightsCalculator.CalculateWeights(input), 1e-6);
        }

        [Test]
        public void InicializeWeights() {
            Assert.AreEqual(new double[] { }, this.weightsCalculator.InicializeWeights(new CoordPoint[] { }));
            Assert.AreEqual(new double[] { double.PositiveInfinity}, this.weightsCalculator.InicializeWeights(new CoordPoint[] { new GeoPoint(0, 0) }));
            Assert.AreEqual(new double[] { double.PositiveInfinity, double.PositiveInfinity }, this.weightsCalculator.InicializeWeights(new CoordPoint[] { new GeoPoint(0, 0), new GeoPoint(1,1) }));
            Assert.AreEqual(new double[] { double.PositiveInfinity, 0.5, 0.5, double.PositiveInfinity }, this.weightsCalculator.InicializeWeights(new CoordPoint[] {
                new GeoPoint(0, 0), new GeoPoint(1, 0), new GeoPoint(1, 1), new GeoPoint(0, 1) }));
        }
    }
}
