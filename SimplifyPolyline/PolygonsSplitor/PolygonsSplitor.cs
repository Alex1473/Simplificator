using DevExpress.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimplifyPolyline.PolygonsSplitor
{
    public class PolygonsSplitor {

        IList<int> chainIds;
        IList<int> pathIds;
        IList<CoordPoint> polygonsPoints;
        IList<int> polygonsLength;

        public void Split(IList<CoordPoint> points, IList<int> polygonsLength) {
            this.polygonsPoints = points;
            this.polygonsLength = polygonsLength;
            this.chainIds = InitializePointChains(points);
            this.pathIds = InitializePathIds(points.Count, polygonsLength);

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
    

        internal IList<int> InitializeHashChains(IList<CoordPoint> polygonsPoints) {
            int hashTableSize = (int)Math.Floor(polygonsPoints.Count * 1.3); //if hashTableSize > 0

            HashGenerator hashGenerator = new HashGenerator();
            hashGenerator.Mod = hashTableSize;
            int[] hashTable = new int[hashTableSize];
            int[] chainIds = new int[polygonsPoints.Count];

            for (int i = 0; i < polygonsPoints.Count; ++i) {
                int key = hashGenerator.Generate(polygonsPoints[i]);
                int previousPointWithSameKeyIndex = hashTable[key] - 1;
                hashTable[key] = i + 1;
                chainIds[i] = previousPointWithSameKeyIndex >= 0 ? previousPointWithSameKeyIndex : i;
            }
            return chainIds;
        }

        internal IList<int> InitializePathIds(int pointsCount,IList<int> polygonsLength) {
            int[] pathIds = new int[pointsCount];
            int j = 0;
            for (int pathId = 0, pathCount = polygonsLength.Count; pathId < pathCount; pathId++) 
                for (int i = 0, n = polygonsLength[pathId]; i < n; i++, j++)
                    pathIds[j] = pathId;
            return pathIds;
        }

        IList<int> convertPaths() {
            List<int> paths = new List<int>();
            int pointId = 0;   

            for (int i = 0, len = this.polygonsLength.Count; i < len; i++) {
                int pathLen = this.polygonsLength[i];
                paths.AddRange(pathLen < 2 ? null : convertPath(pointId, pointId + pathLen - 1));
                pointId += pathLen;
            }
            return paths;
        }

        IList<int> convertPath(int startPath, int endPath) {
            List<int> arcIds = new List<int>();
            int firstNodeId = -1;
            int arcStartId = -1; 

            // Visit each point in the path, up to but not including the last point
            for (var i = startPath; i < endPath; i++) {
                if (PointIsArcEndpoint(i)) {
                    if (firstNodeId > -1) 
                        arcIds.Add(addEdge(arcStartId, i));
                    else 
                        firstNodeId = i;
                    arcStartId = i;
                }
            }

            
            if (firstNodeId == -1) {
                arcIds.Add(addRing(startPath, endPath));
            } else if (firstNodeId == startPath) {
                // path endpoint is a node;
                if (!pointIsArcEndpoint(end)) {
                    error("Topology error"); // TODO: better error handling
                }
                arcIds.push(addEdge(arcStartId, i));
            } else {
                // final arc wraps around
                arcIds.Add(addSplitEdge(arcStartId, endPath, startPath + 1, firstNodeId));
            }
            return arcIds;
        }

        bool PointIsArcEndpoint(int index) {
            int id2 = this.chainIds[index];
            int prev = prevPoint(index);
            int next = nextPoint(index);
            int prev2, next2;
            if (prev == -1 || next == -1) {
                // @id is an endpoint if it is the start or end of an open path
                return true;
            }
            while (index != id2) {
                prev2 = prevPoint(id2);
                next2 = nextPoint(id2);
                if (prev2 == -1 || next2 == -1 || brokenEdge(prev, next, prev2, next2)) 
                    // there is a discontinuity at @id -- point is arc endpoint
                    return true;
                id2 = this.chainIds[id2];
            }
            return false;
        }

        int nextPoint(int pointIndex) {
            int partId = this.pathIds[pointIndex],
            nextId = pointIndex + 1;
            if (nextId < polygonsPoints.Count && this.pathIds[nextId] == partId) {
                return nextId;
            }
            int len = this.polygonsLength[partId];
            return this.polygonsPoints[pointIndex].Equals(this.polygonsPoints[pointIndex - len + 1]) ? pointIndex - len + 2 : -1;
        }

        int prevPoint(int pointIndex) {
            int partId = this.pathIds[pointIndex];
            int prevId = pointIndex - 1;
            if (prevId >= 0 && pathIds[prevId] == partId) {
                return prevId;
            }
            int len = this.polygonsLength[partId];
            return this.polygonsPoints[pointIndex].Equals(this.polygonsPoints[pointIndex + len - 1]) ? pointIndex + len - 2 : -1;
        }

        bool brokenEdge(int aPrev, int aNext, int bPrev, int bNext) {
            CoordPoint aPreviousPoint = this.polygonsPoints[aPrev];
            CoordPoint aNextPoint = this.polygonsPoints[aNext];
            CoordPoint bPreviousPoint = this.polygonsPoints[bPrev];
            CoordPoint bNextPoint = this.polygonsPoints[bNext];

            if (aPreviousPoint.GetX() == bNextPoint.GetX() && aNextPoint.GetX() == bPreviousPoint.GetX() && 
                aPreviousPoint.GetY() == bNextPoint.GetY() && aNextPoint.GetY() == bPreviousPoint.GetY() ||
                aPreviousPoint.GetX() == bPreviousPoint.GetX() && aNextPoint.GetX() == bNextPoint.GetX() && 
                aPreviousPoint.GetY() == bPreviousPoint.GetY() && aNextPoint.GetY() == bPreviousPoint.GetY()) {
                return false;
            }
            return true;
        }
    }
}
