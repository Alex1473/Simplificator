using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline {
    public class WeightedItem {
        MapPath item;
        IList<IList<double>> weights;
        public MapPath Item { get { return this.item;}}

        public IList<IList<double>> Weights { get { return this.weights;}}
        public WeightedItem(MapPath item, IList<IList<double>> weights) {
            this.item = item;
            this.weights = weights;
        }

        
    }
}
