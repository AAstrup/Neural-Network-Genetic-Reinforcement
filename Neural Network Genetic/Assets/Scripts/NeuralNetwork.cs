using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SteerComponent))]
public class NeuralNetwork : MonoBehaviour
{
    public INeuralNetworkInputNode[] inputLayer;
    private List<NeuralNetworkNodeMonoBehaviour> outputLayer;
    private NeuralNetworkNode[] hiddenLayers;
    public ThoughtProcess thoughtProcess;
    private SteerComponent steerComponent;
    [SerializeField]
    private string thoughtProcessStringed;
    [SerializeField]
    private float m_Range = 1f;

    private void Awake()
    {
        steerComponent = GetComponent<SteerComponent>();
        inputLayer = GetComponentsInChildren<INeuralNetworkInputNode>();
        var outputNodes = GetComponentsInChildren<INeuralNetworkOutputNode>();
        outputLayer = new List<NeuralNetworkNodeMonoBehaviour>();
        foreach (var item in outputNodes)
        {
            if (item is NeuralNetworkNodeMonoBehaviour)
                outputLayer.Add(item as NeuralNetworkNodeMonoBehaviour);
        }
    }

    public void Initialize(int m_InputNodes, int m_HiddenLayers, int m_OutputLayers, ThoughtProcess thoughtProcess)
    {
        this.thoughtProcess = thoughtProcess;
        thoughtProcessStringed = thoughtProcess.GetThoughtAsString();
        hiddenLayers = new NeuralNetworkNode[m_HiddenLayers];
        for (int i = 0; i < m_HiddenLayers; i++)
        {
            hiddenLayers[i] = new NeuralNetworkNode();
        }

        foreach (var layer in hiddenLayers)
        {
            layer.SetBias(thoughtProcess.GetNext());
            for (int i = 0; i < inputLayer.Length; i++)
            {
                layer.ConnectTo(inputLayer[i], thoughtProcess.GetNext());
            }
        }

        foreach (var layer in outputLayer)
        {
            layer.SetBias(thoughtProcess.GetNext());
            for (int i = 0; i < hiddenLayers.Length; i++)
            {
                layer.ConnectTo(hiddenLayers[i], thoughtProcess.GetNext());
            }
        }
    }

    void FixedUpdate () {
        float highestActivation = -1f;
        NeuralNetworkNodeMonoBehaviour node = null;
        foreach (var item in outputLayer)
        {
            var value = item.ComputeActivation();
            if(highestActivation < value)
            {
                highestActivation = value;
                node = item;
            }
        }
        // Assumption cast can be made based on how we populate the outputLayer
        (node as INeuralNetworkOutputNode).Activation(highestActivation);
	}
}
