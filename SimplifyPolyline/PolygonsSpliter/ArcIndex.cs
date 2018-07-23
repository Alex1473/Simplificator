using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using SimplifyPolyline.Utils;
using DevExpress.Utils;

namespace SimplifyPolyline.PolygonsSpliter {
    public class ArcIndex {

        readonly int hashTableSize;
        int[] hashTable;
        List<int> chainIds;
        List<IList<CoordPoint>> arcs;
        IList<CoordPoint> points;
        public ArcIndex(IList<CoordPoint> points) {
            this.points = points;
            this.hashTableSize = points.Count / 4 + 1;
            this.hashTable = new int[hashTableSize];
            DifferentUtils.FillArraySameValue(this.hashTable, -1);
            this.chainIds = new List<int>();
            this.arcs = new List<IList<CoordPoint>>();
        }

        public int AddArc(IList<CoordPoint> arc) {
            int key = Math.Abs(HashCodeHelper.CalculateGeneric(arc[arc.Count - 1])) % this.hashTableSize;
            int chainId = hashTable[key];
            int arcId = this.arcs.Count;
            hashTable[key] = arcId;
            arcs.Add(arc);
            chainIds.Add(chainId);
            return arcId;
        }

        internal bool ContainsArcNeighbor(int start, int end, Func<int, int> getNext) {
            int next = getNext(start);
            int key = Math.Abs(HashCodeHelper.CalculateGeneric(this.points[start])) % this.hashTableSize;
            int arcId = hashTable[key];
                
            while (arcId != -1) {
                int arcLength = arcs[arcId].Count;
                if (arcs[arcId][0] == points[end] && arcs[arcId][arcLength - 1] == points[start] && arcs[arcId][arcLength - 2] == points[next])
                    return true;
                arcId = chainIds[arcId];
            }
            return false;
        }

        public bool ContainsMatchingArc(int start, int end, Func<int, int> getNext, Func<int, int> getPrev) {
            if (!ContainsArcNeighbor(start, end, getNext)) 
                return ContainsArcNeighbor(end, start, getPrev);
            return true;
        }

        public PackagedArcs GetVertexData() {
            List<int> arcsLength = new List<int>();
            List<CoordPoint> arcsPoints = new List<CoordPoint>();
            
            for (int i = 0; i < this.arcs.Count; i++) {
                arcsLength.Add(arcs[i].Count);
                arcsPoints.AddRange(arcs[i]);
            }
            return new PackagedArcs(arcsLength, arcsPoints);
        }

    }
}
