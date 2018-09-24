using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunComponent))]
public class NNINRunSpeed : MonoBehaviour, INeuralNetworkInputNode {

    private RunComponent _RunComponent;

    private void Awake()
    {
        _RunComponent = GetComponent<RunComponent>();
    }

    public float ComputeActivation()
    {
        return MathLibrary.Clamb(_RunComponent.m_MoveSpeed);
    }
}
