using DevExpress.Map;
using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline.Tests {
    public static class Utils {
        public static MapPathSegment CreateSegment(int pointsNumber) {
            MapPathSegment segment = new MapPathSegment();
            for (int i = 0; i < pointsNumber; ++i)
                segment.Points.Add(new GeoPoint(0, 0));
            return segment;
        }
    }
}
