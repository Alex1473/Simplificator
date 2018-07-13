using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Map;
using DevExpress.XtraMap;

namespace SimplifyPolyline {
    public interface ISimplificationFilter {
        IEnumerable<MapItem> Filter(IEnumerable<WeightedItem> weightedItems, IList<double> weights, double percent);
    }

    public interface ISimplificationWeightsCalculator {
        void Process(IEnumerable<MapItem> items);
        IList<WeightedItem> WeightedItems { get; }
        IList<double> Weights { get; }
    }

    public interface IWeightsCalculator {
        IList<double> CalculateWeights(IList<CoordPoint> points);
    }
}
