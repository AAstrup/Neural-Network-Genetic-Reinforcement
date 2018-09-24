using System;
using System.Collections.Generic;

[Serializable]
public class NeuralNetworkNode : INeuralNetworkInputNode
{
    private float m_Bias;
    private List<Connection> connections;

    public void SetBias(float bias)
    {
        this.m_Bias = bias;
    }

    public void ConnectTo(INeuralNetworkInputNode node, float weight)
    {
        if (connections == null)
            connections = new List<Connection>();

        connections.Add(new Connection(node, weight));
    }

    public virtual float ComputeActivation()
    {
        float value = 0f;
        foreach (var item in connections)
        {
            value += item.weight * item.node.ComputeActivation();
        }
        return MathLibrary.Clamb(value);
    }
}