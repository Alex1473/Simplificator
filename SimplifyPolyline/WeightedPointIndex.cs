using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline {
    public class WeightedPointIndex : IComparable {
        readonly int index;
        readonly double weight;

        public int Index { get { return this.index; } }
        public double Weight { get { return this.weight; }}
        public WeightedPointIndex(int index, double weight) {
            this.index = index;
            this.weight = weight;
        }

        public int CompareTo(object obj) {
            WeightedPointIndex pointIndex = obj as WeightedPointIndex;
            if (pointIndex == null)
                return -1;
            if (this.Weight < pointIndex.Weight)
                return -1;
            if (this.Weight > pointIndex.Weight)
                return 1;
            if (this.Index < pointIndex.Index)
                return -1;
            if (this.Index > pointIndex.Index)
                return 1;
            return 0;
        }
    }
}
