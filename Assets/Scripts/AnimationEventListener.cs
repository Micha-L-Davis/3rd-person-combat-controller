using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public event System.Action OnHit;


    //These functions are called by animation events
    public void Hit()
    {
        OnHit?.Invoke();
    }
}
