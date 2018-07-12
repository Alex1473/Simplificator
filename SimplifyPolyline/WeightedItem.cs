using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline {
    public class WeightedItem {
        MapItem item;
        IList<IList<double>> weights;
        public MapItem Item {
            get {
                return this.item;
            }
        }

        public IList<IList<double>> Weights {
            get {
                return this.weights;
            }
        }
        public WeightedItem(MapItem item, IList<IList<double>> weights) {
            this.item = item;
            this.weights = weights;
        }
    }
}
