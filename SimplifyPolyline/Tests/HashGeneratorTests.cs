using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DevExpress.XtraMap;
using SimplifyPolyline.PolygonsSplitor;

namespace SimplifyPolyline.Tests.PolygonesSpliter {

    [TestFixture]

    public class HashGeneratorTests {

        HashGenerator hashGenerator;

        [SetUp]
        public void Setuo() {
            this.hashGenerator = new HashGenerator();
        }

        [Test]
        public void Generate() {
            this.hashGenerator.Mod = 37;
            Assert.AreEqual(24, this.hashGenerator.Generate(new GeoPoint(51.26725861266857, 4.047071160507528)));
            Assert.AreEqual(2, this.hashGenerator.Generate(new GeoPoint(51.47502370869813, 4.973991326526914)));
            Assert.AreEqual(22, this.hashGenerator.Generate(new GeoPoint(51.03729848896978, 5.606975945670001)));
        }
    }
}
