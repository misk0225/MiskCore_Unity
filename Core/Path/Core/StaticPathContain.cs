using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.StaticPathGraph
{
    public struct PathVectorIterator
    {
        public List<Vector3> positions;
        public int curIdx;

        public PathVectorIterator(List<Vector3> positions)
        {
            this.positions = positions;
            this.curIdx = 0;
        }

        public PathVectorIterator(List<Transform> transforms)
            : this(transforms.ConvertAll((trans) => trans.position)) { }


        public bool CanNext() => curIdx < positions.Count;
        public Vector3 Next() => positions[curIdx++];
    }
}

