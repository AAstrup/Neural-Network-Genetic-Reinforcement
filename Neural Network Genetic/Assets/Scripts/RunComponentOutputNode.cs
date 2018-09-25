using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunComponentOutputNode : RunComponent, INeuralNetworkOutputNode{
    public void Activation(float activationValue)
    {
        m_MoveSpeed += activationValue * m_AccelerationMultiplier;
        if (m_MoveSpeed < minimumSpeed)
            m_MoveSpeed = minimumSpeed;
    }
}
