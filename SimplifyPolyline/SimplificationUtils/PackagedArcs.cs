using DevExpress.Map;
using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline.PolygonsSpliter {
    public class PackagedArcs {
        readonly IList<int> arcsLength;
        readonly IList<CoordPoint> arcsPoints;

        public IList<int> ArcsLength { get { return this.arcsLength; } }
        public IList<CoordPoint> ArcsPoints { get { return this.arcsPoints; } }

        public PackagedArcs(IList<int> polygonsLength, IList<CoordPoint> polygonsPoints) {
            this.arcsLength = polygonsLength;
            this.arcsPoints = polygonsPoints;
        }

        public PackagedArcs(IEnumerable<MapItem> items) {
            List<CoordPoint> polygonsPoints = new List<CoordPoint>();
            IList<int> polygonsLength = new List<int>();

            foreach (MapItem item in items) {
                MapPath mapPath = item as MapPath;
                if (mapPath == null)
                    continue;
                foreach (MapPathSegment segment in mapPath.Segments) {
                    polygonsLength.Add(segment.Points.Count);
                    foreach (CoordPoint point in segment.Points)
                        polygonsPoints.Add(point);
                }
            }
            this.arcsLength = polygonsLength;
            this.arcsPoints = polygonsPoints;
        }
    }
}
