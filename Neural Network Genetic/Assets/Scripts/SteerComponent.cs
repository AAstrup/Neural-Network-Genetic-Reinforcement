using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerComponent : NeuralNetworkNodeMonoBehaviour, INeuralNetworkOutputNode, INNResetable
{
    [SerializeField]
    private float m_SteerControl;
    [SerializeField]
    private float m_SpawnRotationEuler;
    private static float MaxTurnDirection;

    private void Start()
    {
        NNReset();
    }

    private void Update()
    {
        Activation(-Input.GetAxisRaw("Horizontal"));
    }

    public void Activation(float turnDirection)
    {
        if(Mathf.Abs(turnDirection) > MaxTurnDirection)
        {
            MaxTurnDirection = Mathf.Abs(turnDirection);
        }
        transform.eulerAngles += new Vector3(0, 0, turnDirection * m_SteerControl);
    }

    public void NNReset()
    {
        transform.eulerAngles = new Vector3(0, 0, m_SpawnRotationEuler);
    }
}
