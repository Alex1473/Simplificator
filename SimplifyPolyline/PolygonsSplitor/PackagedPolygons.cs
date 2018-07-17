using DevExpress.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline.PolygonsSplitor {
    public class PackagedPolygons {
        readonly IList<int> polygonsLength;
        readonly IList<CoordPoint> polygonsPoints;

        public IList<int> PolygonsLength { get { return this.polygonsLength; } }
        public IList<CoordPoint> PolygonsPoints { get { return this.polygonsPoints; } }

        public PackagedPolygons(IList<int> polygonsLength, IList<CoordPoint> polygonsPoints) {
            this.polygonsLength = polygonsLength;
            this.polygonsPoints = polygonsPoints;
        }
    }
}
