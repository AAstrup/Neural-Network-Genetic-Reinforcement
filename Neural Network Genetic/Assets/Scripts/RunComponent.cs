using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunComponent : NeuralNetworkNodeMonoBehaviour, INeuralNetworkOutputNode, INNResetable {
    [SerializeField]
    private float m_StartSpeed = 1f;
    [SerializeField]
    private float m_AccelerationMultiplier = 0.1f;
    [SerializeField]
    private float minimumSpeed = 0.4f;
    [HideInInspector]
    public float m_MoveSpeed;

    public void Activation(float activationValue)
    {
        m_MoveSpeed += activationValue * m_AccelerationMultiplier;
        if (m_MoveSpeed < minimumSpeed)
            m_MoveSpeed = minimumSpeed;
    }

    void FixedUpdate () {
        transform.position += transform.right * m_MoveSpeed;
	}

    public void NNReset()
    {
        m_MoveSpeed = m_StartSpeed;
    }
}
