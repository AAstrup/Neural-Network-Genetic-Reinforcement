using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InputNodeRigidBodyVelocity : MonoBehaviour, INeuralNetworkInputNode, INNResetable
{
    private Rigidbody2D m_Rigidbody;
    private float m_AccumilatedActivation = 0f;
    public float maxVelocity = 5f;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    public float ComputeActivation()
    {
        return MathLibrary.Clamb(m_AccumilatedActivation);
    }

    void FixedUpdate ()
    {
        if (m_Rigidbody.velocity.sqrMagnitude > maxVelocity * maxVelocity)
            m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * maxVelocity;
        m_AccumilatedActivation = m_Rigidbody.velocity.magnitude;
	}

    public void NNReset()
    {
        m_AccumilatedActivation = 0f;
    }
}
