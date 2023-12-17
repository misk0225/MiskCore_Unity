using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace MiskCore
{
    public class GlobalSound : SingletonComponent<GlobalSound>
    {
        [NonSerialized]
        public SoundManager Manager;

        protected override void Awake()
        {
            base.Awake();

            Manager = SoundManager.Create("MsikCore.GlobalSound", transform);
            DontDestroyOnLoad(this);
        }
        public SoundManager CreateManager(string name = "SoundManager")
        {
            return SoundManager.Create(name, transform);
        }
    }

}
