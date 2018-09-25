using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunComponentOutputNode))]
public class NNINRunSpeed : MonoBehaviour, INeuralNetworkInputNode {

    private RunComponentOutputNode _RunComponent;

    private void Awake()
    {
        _RunComponent = GetComponent<RunComponentOutputNode>();
    }

    public float ComputeActivation()
    {
        return MathLibrary.Clamb(_RunComponent.m_MoveSpeed);
    }
}
