using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace MiskCore
{
    public class GlobalSoundManager : SingletonComponent<GlobalSoundManager>
    {
        [NonSerialized]
        public SoundManager Scene;

        [NonSerialized]
        public SoundManager Global;

        protected override void Awake()
        {
            base.Awake();

            Scene = CreateManager("MiskCore.SceneSoundManager");
            Global = CreateManager("MiskCore.GlobalSoundManager");
            Scene.transform.parent = transform;
            DontDestroyOnLoad(Global);
        }

        private SoundManager CreateManager(string name)
        {
            GameObject obj = new GameObject(name);
            SoundManager manager = obj.AddComponent<SoundManager>();
            return manager;
        }
    }

}
