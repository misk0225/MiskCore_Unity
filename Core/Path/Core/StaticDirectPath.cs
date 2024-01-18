using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.StaticPathGraph
{
    public class StaticDirectPath
    {
        public List<Vector3> Nodes;

        public StaticNode StartNode { get; private set; }
        public StaticNode EndNode { get; private set; }
        public float Distance
        {
            get
            {
                if (m_Distance == -1)
                    m_Distance = GetDistance();

                return m_Distance;
            }
        }



        private float m_Distance = -1;


        public StaticDirectPath(StaticNode start, StaticNode end, List<Vector3> nodes)
        {
            StartNode = start;
            EndNode = end;
            Nodes = nodes;
        }


        public PathVectorIterator GetForwardPositionIterator() => new PathVectorIterator(new List<Vector3>(Nodes));







        private float GetDistance()
        {
            if (Nodes.Count == 0)
                return Vector3.Distance(StartNode.Position, EndNode.Position);

            float distance = Vector3.Distance(StartNode.Position, Nodes[0]);
            for (int idx = 1; idx < Nodes.Count; idx++)
                distance += Vector3.Distance(Nodes[idx - 1], Nodes[idx]);
            distance += Vector3.Distance(Nodes[Nodes.Count - 1], EndNode.Position);

            return distance;
        }
    }
}

