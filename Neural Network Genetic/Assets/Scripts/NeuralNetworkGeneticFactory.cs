using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[RequireComponent(typeof(NeuralNetworkAgentCollection), typeof(FactoryComponent), typeof(NeuralNetworkGeneticGenerator))]
[RequireComponent(typeof(SpawnPointCollection))]
public class NeuralNetworkGeneticFactory : MonoBehaviour {

    private NeuralNetworkAgentCollection collection;
    private NeuralNetworkGeneticGenerator m_NeuralNetworkGeneticGenerator;
    private FactoryComponent m_Factory;
    [SerializeField]
    private int m_InputNodes = 5;
    [SerializeField]
    private int m_HiddenLayers = 3;
    [SerializeField]
    private int m_OutputLayers = 1;
    [SerializeField]
    private float m_ProcessThoughtRandomnessRange = 1f;

    void Start()
    {
        collection = GetComponent<NeuralNetworkAgentCollection>();
        collection.agentCompleteEvent += AgentComplete;
        m_Factory = GetComponent<FactoryComponent>();
        m_Factory.gameObjectSpawnedEvent += InitializeSpawn;
        m_NeuralNetworkGeneticGenerator = GetComponent<NeuralNetworkGeneticGenerator>();
        m_NeuralNetworkGeneticGenerator.SetupForUse(GetThoughtProcessSize());
        m_NeuralNetworkGeneticGenerator.CreateInitialThoughtProcesses(GetThoughtProcessSize(), m_ProcessThoughtRandomnessRange);
        m_Factory.SpawnAll();
    }

    public int GetThoughtProcessSize()
    {
        return m_HiddenLayers + // Bias
            (m_HiddenLayers * m_InputNodes) + // Weights 
            m_OutputLayers + //Bias 
            (m_OutputLayers * m_HiddenLayers); // Weights 
    }

    private void AgentComplete(NeuralNetworkFittnessSum nnFitness)
    {
        m_NeuralNetworkGeneticGenerator.AgentComplete(nnFitness);
        nnFitness.gameObject.transform.position = transform.position;
    }

    private void InitializeSpawn(GameObject spawn)
    {
        var nn = spawn.GetComponent<NeuralNetwork>();
        if (nn != null)
        {
            InitializeSpawn(nn);
        }
        var resetableGameObject = spawn.GetComponent<DestroyGameObject>();
        if (resetableGameObject != null)
            resetableGameObject.OnResetEvent += m_Factory.SpawnSingle;
    }

    private void InitializeSpawn(NeuralNetwork nn)
    {
        var thoughtProcess = m_NeuralNetworkGeneticGenerator.ProduceChild(GetThoughtProcessSize(), m_ProcessThoughtRandomnessRange);
        nn.Initialize(m_InputNodes, m_HiddenLayers, m_OutputLayers, thoughtProcess);
    }
}
