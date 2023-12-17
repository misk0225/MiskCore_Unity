using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public class SceneSound : SingletonComponent<SceneSound>
    {
        private SoundManager Manager;

        protected override void Awake()
        {
            base.Awake();

            Manager = SoundManager.Create("MsikCore.SceneSound", transform);
            DontDestroyOnLoad(this);
        }

        public SoundManager CreateManager(string name = "SoundManager")
        {
            return SoundManager.Create(name, transform);
        }
    }

}
