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
    public class DouglasPeuckerSimplyfierTests {
        
        DouglasPeuckerSimplyfier douglasPeuckerSimplyfier;

        [SetUp]
        public void Setup() {
            this.douglasPeuckerSimplyfier = new DouglasPeuckerSimplyfier();          
        }

        [Test]
        public void CalculateWeights() {
            double[] weights = this.douglasPeuckerSimplyfier.CalculateWeights(new GeoPoint[] { });
            Assert.AreEqual(0, weights.Length);

            ComparisonHelper.AssertArrays(new double[] { double.PositiveInfinity, double.PositiveInfinity }, 
                this.douglasPeuckerSimplyfier.CalculateWeights(new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1) }));

            GeoPoint[] input = new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(0, 1), new GeoPoint(1, 0), new GeoPoint(0, 0) };
            ComparisonHelper.AssertArrays(new double[] { double.PositiveInfinity, 1,  1,
                double.PositiveInfinity }, this.douglasPeuckerSimplyfier.CalculateWeights(input), 1e-6);

            

            input = new GeoPoint[] {
                new GeoPoint(-1, -3),
                new GeoPoint(-3, 1),
                new GeoPoint(-1, 3),
                new GeoPoint(4, 3),
                new GeoPoint(5, 1),
                new GeoPoint(3, -2),
                new GeoPoint(-1, -3)
            };

            ComparisonHelper.AssertArrays(new double[] { double.PositiveInfinity,  4.0971801, 1.3736056, 4.0971801,
                1.37281294, 2.4327007, double.PositiveInfinity }, this.douglasPeuckerSimplyfier.CalculateWeights(input), 1e-6);

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

            ComparisonHelper.AssertArrays(new double[] { double.PositiveInfinity,  2.2360679, 1.1626367, 3.9958741,
                3.9958741, 1, 1, double.PositiveInfinity }, this.douglasPeuckerSimplyfier.CalculateWeights(input), 1e-6);
        }

        [Test]
        public void FilterPointsByWeight(){
            Assert.AreEqual(new GeoPoint[] { }, this.douglasPeuckerSimplyfier.FilterPointsByWeight(new GeoPoint[] { }, new double[] { }, 100));
            Assert.AreEqual(new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(0, 0) }, 
                this.douglasPeuckerSimplyfier.FilterPointsByWeight(new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(0, 0) }, 
                new double[] { double.PositiveInfinity, double.PositiveInfinity }, 100));

            GeoPoint[] input = new GeoPoint[] {
                new GeoPoint(0, 0),
                new GeoPoint(1, 0),
                new GeoPoint(2, 0),
                new GeoPoint(3, 0),
                new GeoPoint(4, 0),
                new GeoPoint(0, 0),
            };
            double[] weights = new double[] { double.PositiveInfinity, 1, 2, 3, 4, double.PositiveInfinity };

            Assert.AreEqual(new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(4, 0), new GeoPoint(0, 0) }, this.douglasPeuckerSimplyfier.FilterPointsByWeight(input, weights, 10));
            Assert.AreEqual(new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(2, 0), new GeoPoint(3, 0), new GeoPoint(4, 0), new GeoPoint(0, 0)}, 
                this.douglasPeuckerSimplyfier.FilterPointsByWeight(input, weights, 50));
            Assert.AreEqual(input, this.douglasPeuckerSimplyfier.FilterPointsByWeight(input, weights, 100));
            Assert.AreEqual(new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(4, 0), new GeoPoint(0, 0) }, this.douglasPeuckerSimplyfier.FilterPointsByWeight(input, weights, 0));

            weights = new double[] { double.PositiveInfinity, 3, 4, 1, 2, double.PositiveInfinity };
            Assert.AreEqual(new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 0), new GeoPoint(2, 0), new GeoPoint(4, 0), new GeoPoint(0, 0) },
                this.douglasPeuckerSimplyfier.FilterPointsByWeight(input, weights, 74));

            input[5] = new GeoPoint(1, 1);
            Assert.DoesNotThrow(() => this.douglasPeuckerSimplyfier.FilterPointsByWeight(input, weights, 100));
        }

        [Test]
        public void FilterPointsByWeightThrowsExceptionWithIncorrectPercent() {
            GeoPoint[] input = new GeoPoint[] { };
            double[] weights = new double[] { };
            Assert.Throws<ArgumentException>(() => this.douglasPeuckerSimplyfier.FilterPointsByWeight(input, weights, 101));
            Assert.Throws<ArgumentException>(() => this.douglasPeuckerSimplyfier.FilterPointsByWeight(input, weights, -1));
        }

        [Test]
        public void Simplify() {
            Assert.AreEqual(new GeoPoint[] { }, this.douglasPeuckerSimplyfier.Simplify(new GeoPoint[] { }, 100));

            GeoPoint[] input = new GeoPoint[] { new GeoPoint(0, 0), new GeoPoint(1, 1), new GeoPoint(0, 0) };
            Assert.AreEqual(input, this.douglasPeuckerSimplyfier.Simplify(input, 0));
            Assert.AreEqual(input, this.douglasPeuckerSimplyfier.Simplify(input, 100));

            input = new GeoPoint[] {
                new GeoPoint(0, 0),
                new GeoPoint(1, 0),
                new GeoPoint(2, 0),
                new GeoPoint(3, 0),
                new GeoPoint(4, 0),
                new GeoPoint(0, 0),
            };

            Assert.GreaterOrEqual(this.douglasPeuckerSimplyfier.Simplify(input, 100).Length, this.douglasPeuckerSimplyfier.Simplify(input, 100).Length);
            Assert.GreaterOrEqual(this.douglasPeuckerSimplyfier.Simplify(input, 75).Length, this.douglasPeuckerSimplyfier.Simplify(input, 25).Length);
        }
    }
}
