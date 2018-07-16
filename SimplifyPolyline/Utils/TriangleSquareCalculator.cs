using DevExpress.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline.Utils
{
    public class TriangleSquareCalculator {
        public double CalculateSquare(CoordPoint a, CoordPoint b, CoordPoint c) {
            return 0.5 * Math.Abs((a.GetX() - b.GetX()) * (c.GetY() - b.GetY()) - (c.GetX() - b.GetX()) * (a.GetY() - b.GetY()));
        }
    }
}
