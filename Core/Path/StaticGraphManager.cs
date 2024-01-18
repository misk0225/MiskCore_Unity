using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.StaticPathGraph
{
    public class StaticGraphManager : SingletonComponent<StaticGraphManager>
    {
        [SerializeField] private List<string> names;
        [SerializeField] private List<StaticGraphObject> graphFactorys;

        private Dictionary<string, StaticGraph> graphMap = new Dictionary<string, StaticGraph>();

        protected override void Awake()
        {
            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];
                StaticGraphObject factory = graphFactorys[i];
                graphMap.Add(name, factory.GetGraph());
            }
        }




        public StaticGraph GetGraphByName(string name) => graphMap[name];



    }
}

