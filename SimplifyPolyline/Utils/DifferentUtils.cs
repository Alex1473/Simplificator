using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Map;
using DevExpress.XtraMap;

namespace SimplifyPolyline.Utils {
    public static class DifferentUtils {
        public static MapPathSegment CreateSegment(int pointsNumber) {
            MapPathSegment segment = new MapPathSegment();
            for (int i = 0; i < pointsNumber; ++i)
                segment.Points.Add(new GeoPoint(i, i));
            return segment;
        }

        public static void FillArraySameValue(int[] array, int value) {
            for (int i = 0; i < array.Length; ++i)
                array[i] = value;
        }

        public static IList<CoordPoint> GetSubSequence(IList<CoordPoint> sequence, int start, int end) {
            IList<CoordPoint> subSequance = new List<CoordPoint>();
            for (int i = start; i < end; ++i)
                subSequance.Add(sequence[i]);
            return subSequance;
        }
    }
}
