using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using DevExpress.XtraMap;


namespace SimplifyPolyline {
    public interface ISimplifyPolyline {
        CoordPoint[] Simplify(CoordPoint[] points, double accuracy);
    }
}
