using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;


namespace MiskCore.Playables.Module.IdleBase
{
    /// <summary>
    /// �Τ@�� clip ��@�ݾ��ʵe
    /// </summary>
    public class ClipIdlePlayable : IIdlePlayable
    {
        public Playable Playable { get; private set; }
        public float Speed { get 
            {
                return _Speed;
            } 
            set 
            {
                _Speed = value;
                Playable.SetSpeed(_Speed);
            } 
        }

        private float _Speed;

        public ClipIdlePlayable(PlayableGraph graph, AnimationClip clip)
        {
            Playable = AnimationClipPlayable.Create(graph, clip);
            _Speed = 1f;
        }
    }

}
