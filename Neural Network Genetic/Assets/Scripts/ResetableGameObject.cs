using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resets all components that implements INNResetable
/// </summary>
public class ResetableGameObject : MonoBehaviour {
    private INNResetable[] resetablesComponents;
    public delegate void ResetEvent();
    public ResetEvent OnResetEvent;

    void Awake () {
        resetablesComponents = GetComponentsInChildren<INNResetable>();
        GetComponent<CallbackOnCollisionComponent>().onCollisionEvent += Reset;
    }

    public void Reset()
    {
        foreach (var item in resetablesComponents)
        {
            item.NNReset();
        }
        if (OnResetEvent != null)
            OnResetEvent();
    }
}
