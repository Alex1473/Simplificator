using DevExpress.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplifyPolyline.Utils;
using DevExpress.Utils;


namespace SimplifyPolyline.PolygonsSpliter {
    public class PolygonsSpliter {

        IList<int> chainIds;
        IList<int> pathIds;
        IList<CoordPoint> polygonsPoints;
        IList<int> polygonsLength;
        ArcIndex arcIndex;

        public PackagedArcs Split(PackagedArcs packagedPolygons) {
            this.polygonsPoints = packagedPolygons.ArcsPoints;
            this.polygonsLength = packagedPolygons.ArcsLength;
            this.chainIds = InitializePointChains(packagedPolygons.ArcsPoints);
            this.pathIds = InitializePathIds(packagedPolygons.ArcsLength);
            this.arcIndex = new ArcIndex(packagedPolygons.ArcsPoints);
            return ConvertPaths();
        }

        internal IList<int> InitializePointChains(IList<CoordPoint> polygonsPoints) {
            IList<int> chainIds = InitializeHashChains(polygonsPoints);
            int j;
            
            for (int i = polygonsPoints.Count - 1; i >= 0; i--) {
                int next = chainIds[i];

                if (next >= i)
                    continue;
                int prevMatchId = i;
                int prevUnmatchId = -1;
                do {
                    j = next;
                    next = chainIds[j];
                    if (polygonsPoints[i] == polygonsPoints[j]) {
                        chainIds[j] = prevMatchId;
                        prevMatchId = j;
                    } else {
                        if (prevUnmatchId > -1) 
                            chainIds[prevUnmatchId] = j;
                        prevUnmatchId = j;
                    }
                } while (next < j);

                if (prevUnmatchId > -1)
                    chainIds[prevUnmatchId] = prevUnmatchId;
                chainIds[i] = prevMatchId;
            }
            return chainIds;
        }
    
        IList<int> InitializeHashChains(IList<CoordPoint> polygonsPoints) {
            int hashTableSize = (int)Math.Floor(polygonsPoints.Count * 1.3); //if hashTableSize > 0
            int[] hashTable = new int[hashTableSize];
            int[] chainIds = new int[polygonsPoints.Count];

            for (int i = 0; i < polygonsPoints.Count; ++i) {
                int key = Math.Abs(HashCodeHelper.CalculateGeneric(polygonsPoints[i])) % hashTableSize;
                int previousPointWithSameKeyIndex = hashTable[key] - 1;
                hashTable[key] = i + 1;
                chainIds[i] = previousPointWithSameKeyIndex >= 0 ? previousPointWithSameKeyIndex : i;
            }
            return chainIds;
        }

        internal IList<int> InitializePathIds(IList<int> polygonsLength) {
            IList<int> pathIds = new List<int>();
            int j = 0;
            for (int pathId = 0, pathCount = polygonsLength.Count; pathId < pathCount; pathId++) 
                for (int i = 0, n = polygonsLength[pathId]; i < n; i++, j++)
                    pathIds.Add(pathId);
            return pathIds;
        }

        PackagedArcs ConvertPaths() {
            int pointId = 0;   
            for (int i = 0, len = this.polygonsLength.Count; i < len; i++) {
                int pathLen = this.polygonsLength[i];
                ConvertPath(pointId, pointId + pathLen - 1);
                pointId += pathLen;
            }
            return this.arcIndex.GetVertexData();
        }

        void ConvertPath(int startPath, int endPath) {
            int firstNodeId = -1;
            int arcStartId = -1;

            for (int i = startPath; i < endPath; i++) 
                if (PointIsArcEndpoint(i)) {
                    if (firstNodeId > -1) 
                        AddEdge(arcStartId, i);
                    else 
                        firstNodeId = i;
                    arcStartId = i;
                }

            if (firstNodeId == -1) {
                AddRing(startPath, endPath);
                return;
            }
            if (firstNodeId == startPath) 
                AddEdge(arcStartId, endPath);
            else 
                AddSplitEdge(arcStartId, endPath, startPath + 1, firstNodeId);
        }

        internal bool PointIsArcEndpoint(int index) {
            int id2 = this.chainIds[index];
            int prev = CalculatePreviousPointIndex(index);
            int next = CalculateNextPointIndex(index);
            if (prev == -1 || next == -1) 
                return true;
            
            while (index != id2) {
                int prev2 = CalculatePreviousPointIndex(id2);
                int next2 = CalculateNextPointIndex(id2);
                if (prev2 == -1 || next2 == -1 || BrokenEdge(prev, next, prev2, next2)) 
                    return true;
                id2 = this.chainIds[id2];
            }
            return false;
        }

        internal int CalculateNextPointIndex(int pointIndex) {
            int partId = this.pathIds[pointIndex];
            if (pointIndex + 1 < polygonsPoints.Count && this.pathIds[pointIndex + 1] == partId) 
                return pointIndex + 1;
            
            int len = this.polygonsLength[partId];
            if (len == 1)
                return -1;
            return this.polygonsPoints[pointIndex].Equals(this.polygonsPoints[pointIndex - len + 1]) ? pointIndex - len + 2 : -1;
        }

        internal int CalculatePreviousPointIndex(int pointIndex) {
            int partId = this.pathIds[pointIndex];
            int prevId = pointIndex - 1;
            if (prevId >= 0 && pathIds[prevId] == partId) 
                return prevId;
            
            int len = this.polygonsLength[partId];
            if (len == 1)
                return -1;
            return this.polygonsPoints[pointIndex].Equals(this.polygonsPoints[pointIndex + len - 1]) ? pointIndex + len - 2 : -1;
        }

        bool BrokenEdge(int aPrev, int aNext, int bPrev, int bNext) {
            CoordPoint aPreviousPoint = this.polygonsPoints[aPrev];
            CoordPoint aNextPoint = this.polygonsPoints[aNext];
            CoordPoint bPreviousPoint = this.polygonsPoints[bPrev];
            CoordPoint bNextPoint = this.polygonsPoints[bNext];

            return !((aPreviousPoint == bNextPoint && aNextPoint == bPreviousPoint) || (aPreviousPoint == bPreviousPoint) && (aNextPoint == bNextPoint)); 
        }

        void AddEdge(int start, int end) {
            if (!this.arcIndex.ContainsMatchingArc(start, end, this.CalculateNextPointIndex, this.CalculatePreviousPointIndex))
                this.arcIndex.AddArc(DifferentUtils.GetSubSequence(this.polygonsPoints, start, end + 1)); 
        }

        void AddRing(int startId, int endId) {
            int chainId = this.chainIds[startId];
            int pathId = this.pathIds[startId];

            while (chainId != startId) {
                if (pathIds[chainId] < pathId) 
                    break;
                chainId = chainIds[chainId];
            }

            if (chainId == startId) {
                AddEdge(startId, endId);
                return;
            }

            for (int i = startId; i < endId; i++) 
                if (!this.arcIndex.ContainsMatchingArc(i, i, this.CalculateNextPointIndex, this.CalculatePreviousPointIndex))
                    return;
           
            throw new Exception("Unmatched ring");
        }
        void AddSplitEdge(int startEdge1, int endEdge1, int startEdge2, int endEdge2) {
            if (!arcIndex.ContainsMatchingArc(startEdge1, endEdge2, CalculateNextPointIndex, CalculatePreviousPointIndex)) 
                this.arcIndex.AddArc(MergeArcParts(startEdge1, endEdge1, startEdge2, endEdge2));            
        }

        internal IList<CoordPoint> MergeArcParts(int startEdge1, int endEdge1, int startEdge2, int endEdge2) {
            IList<CoordPoint> resultArc = new List<CoordPoint>();
            for (int i = startEdge1; i <= endEdge1; i++)
                resultArc.Add(this.polygonsPoints[i]);
                
            for (int i = startEdge2; i <= endEdge2; i++) {
                resultArc.Add(this.polygonsPoints[i]);
            }
            return resultArc;
        }
    }
}
