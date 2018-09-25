using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackOnCollisionComponent : MonoBehaviour, INNResetable {

    public delegate void OnCollisionEvent(Collision2D collision);
    public OnCollisionEvent onCollisionEvent;
    private bool m_Triggered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_Triggered)
        {
            m_Triggered = true;
            onCollisionEvent(collision);
        }
    }

    public void NNReset()
    {
        m_Triggered = false;
    }
}
