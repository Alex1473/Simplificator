using DevExpress.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyPolyline.PolygonsSplitor
{
    public class HashGenerator {
        
        public Int32 Mod { get; set; }
    
        public Int32 Generate(CoordPoint point) {
            byte[] xBytes = BitConverter.GetBytes(point.GetX());
            byte[] yBytes = BitConverter.GetBytes(point.GetY());
            Int32 hash = BitConverter.ToInt32(xBytes, 0) ^ BitConverter.ToInt32(xBytes, 4);
            hash = hash << 5 ^ hash >> 7 ^ BitConverter.ToInt32(yBytes, 0) ^ BitConverter.ToInt32(yBytes, 4);
            return (hash & 0x7fffffff) % this.Mod;
        }
    }
}
