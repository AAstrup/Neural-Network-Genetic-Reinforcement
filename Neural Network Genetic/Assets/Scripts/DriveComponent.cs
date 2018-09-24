using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DriveComponent : NeuralNetworkNodeMonoBehaviour, INeuralNetworkOutputNode, INNResetable
{
    [SerializeField]
    private float m_StartSpeed = 1f;
    [SerializeField]
    private float m_AccelerationMultiplier = 0.1f;
    [SerializeField]
    private float minimumSpeed = 0.4f;
    private Rigidbody2D m_Body;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
        SetStartSpeed();
    }

    public void Activation(float activationValue)
    {
        m_Body.AddForce(transform.right * activationValue * m_AccelerationMultiplier);
        if (m_Body.velocity.magnitude < minimumSpeed)
            m_Body.velocity = m_Body.velocity * minimumSpeed;
    }

    public void NNReset()
    {
        SetStartSpeed();
    }

    private void SetStartSpeed()
    {
        m_Body.velocity = transform.right * m_StartSpeed;
    }
}
