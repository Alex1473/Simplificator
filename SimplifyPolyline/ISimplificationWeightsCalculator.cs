using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline {
    public interface ISimplificationWeightsCalculator {
        void Process(IEnumerable<MapItem> items);
        IList<WeightedItem> WeightedItems { get; }
        IList<double> Weights { get; }
    }
}
