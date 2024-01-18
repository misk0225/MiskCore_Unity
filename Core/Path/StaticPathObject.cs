using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.StaticPathGraph
{
    public class StaticPathObject : MonoBehaviour
    {
        [SerializeField] private StaticNodeObject m_StartNode;
        [SerializeField] private StaticNodeObject m_EndNode;
        [SerializeField] private List<Transform> nodes;
        [SerializeField] private bool m_CanReverse = true;

        public bool CanReverce => m_CanReverse;

        public StaticDirectPath GetForwardPath()
            => new StaticDirectPath(m_StartNode.GetNode(), m_EndNode.GetNode(), nodes.ConvertAll((trans) => trans.position));

        public StaticDirectPath GetBackwardPath()
        {
            List<Vector3> lst = nodes.ConvertAll((trans) => trans.position);
            lst.Reverse();

            return new StaticDirectPath(m_EndNode.GetNode(), m_StartNode.GetNode(), lst);
        }
    }
}




