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
    public bool debugPrint;

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

    public void Initialize(int m_HiddenLayers, ThoughtProcess thoughtProcess)
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

        if (!thoughtProcess.HasNoNext())
            Debug.LogWarning("INputnodes, hidden nodes, or output node amount is wrong, is at " + thoughtProcess.GetCurrentIndex() + " but expected " + thoughtProcess.biasAndWeights.Length);
    }

    void FixedUpdate () {
        float bestValue = Mathf.NegativeInfinity;
        INeuralNetworkOutputNode node = null;
        if(debugPrint)
            Debug.Log("Start ------ ");
        foreach (var item in outputLayer)
        {
            var value = item.ComputeActivation();
            // Assumption cast can be made based on how we populate the outputLayer
            if (value > bestValue)
            {
                node = item as INeuralNetworkOutputNode;
                bestValue = value;
            }
            if(debugPrint)
                Debug.Log("item " + item.GetType() + " returns value " + value);
        }
        node.Activation(bestValue);
    }
}
