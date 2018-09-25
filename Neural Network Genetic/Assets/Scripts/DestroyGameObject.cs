using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resets all components that implements INNResetable
/// </summary>
public class DestroyGameObject : MonoBehaviour {
    private INNResetable[] resetablesComponents;
    public delegate void ResetEvent();
    public ResetEvent OnResetEvent;
    public bool triggered;
    public bool DestroyOnCollision;

    void Awake () {
        resetablesComponents = GetComponentsInChildren<INNResetable>();
        if(DestroyOnCollision)
            GetComponent<CallbackOnCollisionComponent>().onCollisionEvent += Trigger;
    }

    public void Trigger(Collision2D collision)
    {
        Trigger();
    }

    public void Trigger()
    {
        if (triggered)
            return;
        foreach (var item in resetablesComponents)
        {
            item.NNReset();
        }
        if (OnResetEvent != null)
            OnResetEvent();
        triggered = true;
        Destroy(gameObject);
    }
}
