using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.StaticPathGraph
{
    public class StaticNodeObject : MonoBehaviour
    {
        [SerializeField] private string m_name = "default";


        public StaticNode GetNode() => new StaticNode(m_name, transform.position);
    }
}

