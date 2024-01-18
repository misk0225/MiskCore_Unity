using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.StaticPathGraph
{
    public static class PathGraphExtension
    {
        public static void SetTransformToNode(this StaticGraph graph, Transform transform, string nodeName)
        {
            transform.position = graph.GetNodeByName(nodeName).Position;
        }

        public static void AddPath(this StaticGraph graph, StaticPathObject pathObject)
        {
            graph.AddPath(pathObject.GetForwardPath());
            if (pathObject.CanReverce)
                graph.AddPath(pathObject.GetBackwardPath());
        }
    }
}

