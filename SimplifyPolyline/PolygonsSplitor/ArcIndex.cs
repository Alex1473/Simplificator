using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Map;
using SimplifyPolyline.Utils;

namespace SimplifyPolyline.PolygonsSplitor
{
    public class ArcIndex {

        readonly int hashTableSize;
        HashGenerator hashGenerator;
        int[] hashTable;
        List<int> chainIds;
        List<IList<CoordPoint>> arcs;
        int arcPoints;
        IList<CoordPoint> points;
        public ArcIndex(IList<CoordPoint> points) {
            this.points = points;
            this.hashTableSize = (int)Math.Floor(points.Count * 0.25 + 1);
            this.hashGenerator = new HashGenerator();
            this.hashGenerator.Mod = hashTableSize;
            this.hashTable = new int[hashTableSize];
            DifferentUtils.FillArraySameValue(this.hashTable, -1);
            this.chainIds = new List<int>();
            this.arcs = new List<IList<CoordPoint>>();
            this.arcPoints = 0;
        }

        public int AddArc(IList<CoordPoint> arc) {
            int key = this.hashGenerator.Generate(arc[arc.Count - 1]);
            int chainId = hashTable[key];
            int arcId = this.arcs.Count;
            hashTable[key] = arcId;
            arcs.Add(arc);
            this.arcPoints += arc.Count;
            chainIds.Add(chainId);
            return arcId;
        }

        internal int findArcNeighbor(int start, int end, Func<int, int> getNext) {
            int next = getNext(start);
            int key = this.hashGenerator.Generate(this.points[start]);
            int arcId = hashTable[key];
                
            while (arcId != -1) {
                int arcLength = arcs[arcId].Count;
                if (arcs[arcId][0] == points[end] && arcs[arcId][arcLength - 1] == points[start] && arcs[arcId][arcLength - 2] == points[next])
                    return arcId;
                arcId = chainIds[arcId];
            }
            return -1;
        }

        public int FindMatchingArc(int start, int end, Func<int, int> getNext, Func<int, int> getPrev) {
            int arcId = findArcNeighbor(start, end, getNext);
            if (arcId == -1) {
                arcId = findArcNeighbor(end, start, getPrev);
                arcId = arcId == -1 ? 1 : arcId;
            } else
                arcId = -(arcId + 1);

            
            return arcId;
        }

        public PackagedPolygons GetVertexData() {
            List<int> arcsLength = new List<int>();
            List<CoordPoint> arcsPoints = new List<CoordPoint>();
            
            for (int i = 0; i < this.arcs.Count; i++) {
                arcsLength.Add(arcs[i].Count);
                arcsPoints.AddRange(arcs[i]);
            }
            return new PackagedPolygons(arcsLength, arcsPoints);
        }

    }
}
