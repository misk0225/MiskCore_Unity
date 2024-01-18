using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.StaticPathGraph
{
    public class StaticGraph
    {
        private Dictionary<string, StaticNode> m_NodeMap = new Dictionary<string, StaticNode>();

        public void AddPath(StaticDirectPath path)
        {
            AddToNodeMap(path);
            UpdateGraph();
        }

        public void AddPaths(List<StaticDirectPath> paths)
        {
            foreach (StaticDirectPath path in paths)
                AddToNodeMap(path);

            UpdateGraph();
        }

        public StaticNode GetNodeByName(string name) => m_NodeMap[name];

        public StaticDirectPath GetShortestPath(string from, string to)
        {
            StortPathCalculater calculater = new StortPathCalculater(this);
            return calculater.GetShort(m_NodeMap[from], m_NodeMap[to]);
        }



        private void AddToNodeMap(StaticDirectPath path)
        {
            m_NodeMap.TryAdd(path.StartNode.Name, path.StartNode);
            m_NodeMap.TryAdd(path.EndNode.Name, path.EndNode);

            StaticNode start = m_NodeMap[path.StartNode.Name];
            StaticNode end = m_NodeMap[path.EndNode.Name];

            start.AddNearNode(end, path);
        }


        private void UpdateGraph() { }













        public class StortPathCalculater
        {
            private StaticGraph Graph;
            private Dictionary<StaticNode, NodeVisitData> VisitDataMap;


            public StortPathCalculater(StaticGraph graph)
            {
                Graph = graph;
            }

            public StaticDirectPath GetShort(StaticNode from, StaticNode to)
            {
                UpdateVisitData(from, to);
                return GetPath(from, to);
            }

            private void InitVisitDataMap()
            {
                VisitDataMap = new Dictionary<StaticNode, NodeVisitData>();
                foreach (StaticNode node in Graph.m_NodeMap.Values)
                    VisitDataMap.Add(node, new NodeVisitData(false, float.MaxValue, null));
            }

            private void UpdateVisitData(StaticNode from, StaticNode to)
            {
                InitVisitDataMap();
                var fromData = VisitDataMap[from];
                fromData.cost = 0;
                VisitDataMap[from] = fromData;

                StaticNode cur = from;
                while (cur != null && cur != to)
                {
                    GoNearAndUpdateCost(cur);
                    cur = GetNewVisitNode();
                }
            }

            private void GoNearAndUpdateCost(StaticNode node)
            {
                NodeVisitData data = VisitDataMap[node];
                data.isVisit = true;
                VisitDataMap[node] = data;

                foreach (StaticNode near in node.GetNears())
                {
                    NodeVisitData Ndata = VisitDataMap[near];
                    if (Ndata.isVisit)
                        continue;

                    float newCost = data.cost + node.GetNearPath(near).Distance;
                    if (newCost > Ndata.cost)
                        continue;

                    Ndata.cost = newCost;
                    Ndata.parent = node;
                    VisitDataMap[near] = Ndata;
                }
            }

            private StaticNode GetNewVisitNode()
            {
                StaticNode result = null;
                float min = float.MaxValue;
                foreach (StaticNode node in VisitDataMap.Keys)
                {
                    NodeVisitData data = VisitDataMap[node];
                    if (data.isVisit)
                        continue;

                    if (data.cost < min)
                    {
                        result = node;
                        min = data.cost;
                    }
                }

                return result;
            }

            private StaticDirectPath GetPath(StaticNode from, StaticNode to)
            {
                List<Vector3> positions = new List<Vector3>();

                StaticNode cur = to;
                positions.Add(cur.Position);

                while (cur.Name != from.Name)
                {
                    StaticNode node = VisitDataMap[cur].parent;
                    positions.AddRange(cur.GetNearPath(node).Nodes);
                    positions.Add(node.Position);

                    cur = node;
                }

                positions.Reverse();
                return new StaticDirectPath(from, to, positions);
            }

            private void LogPath(StaticNode from, StaticNode to)
            {
                StaticNode cur = to;
                Debug.Log(cur.Name);

                while (cur.Name != from.Name)
                {
                    StaticNode node = VisitDataMap[cur].parent;
                    Debug.Log(node.Name);

                    cur = node;
                }
            }





            private struct NodeVisitData
            {
                public bool isVisit;
                public float cost;
                public StaticNode parent;

                public NodeVisitData(bool isVisit, float cost, StaticNode parent)
                {
                    this.isVisit = isVisit;
                    this.cost = cost;
                    this.parent = parent;
                }
            }
        }
    }
}

