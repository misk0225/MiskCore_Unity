using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiskCore.StaticPathGraph
{
    public class StaticNode
    {
        public Vector3 Position { get; private set; }
        public string Name { get; private set; }

        private Dictionary<string, StaticNode> NearNodes = new Dictionary<string, StaticNode>();
        private Dictionary<string, StaticDirectPath> NearPaths = new Dictionary<string, StaticDirectPath>();


        public StaticNode(string name, Vector3 position)
        {
            Name = name;
            Position = position;
        }

        public void AddNearNode(StaticNode node, StaticDirectPath path)
        {
            NearNodes.TryAdd(node.Name, node);
            NearPaths.TryAdd(node.Name, path);
        }

        public StaticNode GetNearNodeByName(string name) => NearNodes[name];
        public StaticDirectPath GetNearPath(StaticNode node) => NearPaths[node.Name];

        public List<StaticNode> GetNears()
        {
            List<StaticNode> lst = new List<StaticNode>();
            foreach (StaticNode node in NearNodes.Values)
                lst.Add(node);

            return lst;
        }
    }
}

