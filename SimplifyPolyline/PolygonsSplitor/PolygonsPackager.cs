using DevExpress.Map;
using DevExpress.XtraMap;
using SimplifyPolyline.PolygonsSplitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline.PolygonsSplitor
{
    public class PolygonsPackager {
        public PackagedPolygons PackPolygons(IEnumerable<MapItem> items) {
            List<CoordPoint> polygonsPoints = new List<CoordPoint>();
            IList<int> polygonsLength = new List<int>();

            foreach(MapItem item in items) {
                MapPath mapPath = item as MapPath;
                if (mapPath == null)
                    continue;
                foreach(MapPathSegment segment in mapPath.Segments) {
                    polygonsLength.Add(segment.Points.Count);
                    foreach(CoordPoint point in segment.Points)
                        polygonsPoints.Add(point);
                }
            }
            return new PackagedPolygons(polygonsLength, polygonsPoints);
        }
    }
}
