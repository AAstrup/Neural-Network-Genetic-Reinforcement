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

    void Awake () {
        resetablesComponents = GetComponentsInChildren<INNResetable>();
        GetComponent<CallbackOnCollisionComponent>().onCollisionEvent += Reset;
    }

    public void Reset()
    {
        //foreach (var item in resetablesComponents)
        //{
        //    item.NNReset();
        //}
        //if (OnResetEvent != null)
        //    OnResetEvent();
        if (triggered)
            return;
        if (OnResetEvent != null)
            OnResetEvent();
        triggered = true;
        Destroy(gameObject);
    }
}
