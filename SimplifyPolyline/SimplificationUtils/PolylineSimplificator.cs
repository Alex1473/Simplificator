using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraMap;

namespace SimplifyPolyline
{
    public class PolylineSimplificator {
        ISimplificationFilter simplificationFilter;
        ISimplificationWeightsCalculator weightsCalculator;

        public PolylineSimplificator(ISimplificationWeightsCalculator weightsCalculator, ISimplificationFilter simplificationFilter) {
            this.simplificationFilter = simplificationFilter;
            this.weightsCalculator = weightsCalculator;
        }

        public void Prepare(IEnumerable<MapItem> items) {
            this.weightsCalculator.Process(items);
        }        
        public IEnumerable<MapItem> Simplify(double percent) {
            return this.simplificationFilter.Filter(this.weightsCalculator.WeightedItems, this.weightsCalculator.Weights, percent);
        }
    }
}
