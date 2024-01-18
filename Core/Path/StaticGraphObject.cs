using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.StaticPathGraph
{
    public class StaticGraphObject : MonoBehaviour
    {
        [SerializeField] private Transform paths;


        public StaticGraph GetGraph()
        {
            StaticGraph graph = new StaticGraph();
            foreach (Transform child in paths)
                graph.AddPath(child.GetComponent<StaticPathObject>());

            return graph;
        }

    }
}

