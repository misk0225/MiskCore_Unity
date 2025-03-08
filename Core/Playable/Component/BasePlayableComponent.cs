using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public abstract class BasePlayableComponent : MonoBehaviour
{

    public PlayableGraph Graph { get; private set; }

    public PlayableOutput PlayableOutput { get; private set; }



    protected Animator _Animator;


    #region Unity EventCallback

    protected virtual void Awake()
    {
        _Animator = GetComponent<Animator>();
        Graph = PlayableGraph.Create();
        PlayableOutput = AnimationPlayableOutput.Create(Graph, "output", _Animator);
    }
    protected virtual void OnDestroy()
    {
        Graph.Destroy();
    }

    #endregion

    public void SetRootPlayable(Playable playable)
    {
        playable.SetInputCount(2);

        if (playable.CanSetWeights())
        {
            playable.SetInputWeight(0, 1);
            playable.SetInputWeight(1, 0);
        }

        PlayableOutput.SetSourcePlayable(playable);
    }



    public abstract void Pause();

    public abstract void Continue();

}
