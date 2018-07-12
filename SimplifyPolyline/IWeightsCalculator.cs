using DevExpress.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline
{
    public interface IWeightsCalculator {
        IList<double> CalculateWeights(IList<CoordPoint> points);
    }
}
