using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase.Namble
{
    [CreateAssetMenu(fileName = "NamableAction", menuName = "ScriptObjects/MiskCore/Playable/IdleBase/Namble/Clip")]
    public class ClipNamableActionScriptableObject : NamableActionScriptableObject
    {

        [System.Serializable]
        internal class Info
        {
            public string Name;
            public AnimationClip Clip;
            public float StartMix;
            public float ExitMix;
            public float Speed = 1f;
        }

        [SerializeField]
        private List<Info> _Infos;


        public override IActionsPlayableNamable GetNamableAction(PlayableGraph graph)
        {
            ClipNamableAction namableActions = new ClipNamableAction(graph);

            foreach (var info in _Infos)
            {
                namableActions.AddClip(info.Name, info.Clip, info.StartMix, info.ExitMix, info.Speed);
            }

            return namableActions;
        }
    }
}

