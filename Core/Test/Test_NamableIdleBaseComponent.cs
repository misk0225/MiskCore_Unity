using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.Playables.Module.IdleBase.Namble
{
    public class Test_NamableIdleBaseComponent : MonoBehaviour
    {
        public NamableIdleBaseComponent Com;

        public void ActionByName(string name)
        {
            Com.ActionByName(name);
        }
    }

}
