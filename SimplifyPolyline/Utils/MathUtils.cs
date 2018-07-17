using DevExpress.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline.Utils
{
    public static class MathUtils {
        internal static double CalculateSquareDistance(CoordPoint a, CoordPoint b) {
            double dx = b.GetX() - a.GetX();
            double dy = b.GetY() - a.GetY();
            return dx * dx + dy * dy;
        }
        public static double CalculateSquareDistanceFromPointToSegment(CoordPoint firstSegmentPoint, CoordPoint secondSegmentPoint, CoordPoint point) {
            if (firstSegmentPoint == secondSegmentPoint)
                return CalculateSquareDistance(firstSegmentPoint, point);

            double acSquare = CalculateSquareDistance(firstSegmentPoint, secondSegmentPoint);
            double abSquare = CalculateSquareDistance(firstSegmentPoint, point);
            double bcSquare = CalculateSquareDistance(secondSegmentPoint, point);

            if (abSquare >= bcSquare + acSquare)
                return bcSquare;

            if (bcSquare >= abSquare + acSquare)
                return abSquare;

            double interimValue = abSquare + acSquare - bcSquare;
            double distance = abSquare - interimValue * interimValue / acSquare * 0.25;
            return Math.Max(distance, 0);
        }

        public static double CalculateSquare(CoordPoint a, CoordPoint b, CoordPoint c) {
            return 0.5 * Math.Abs((a.GetX() - b.GetX()) * (c.GetY() - b.GetY()) - (c.GetX() - b.GetX()) * (a.GetY() - b.GetY()));
        }
    }
}
