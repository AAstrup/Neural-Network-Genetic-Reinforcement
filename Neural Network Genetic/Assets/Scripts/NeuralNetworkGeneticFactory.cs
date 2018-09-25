using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[RequireComponent(typeof(FactoryComponent))]
[RequireComponent(typeof(SpawnPointCollection))]
public class NeuralNetworkGeneticFactory : MonoBehaviour {

    [SerializeField]
    private NeuralNetworkAgentCollection collection;
    [SerializeField]
    private NeuralNetworkGeneticGenerator m_NeuralNetworkGeneticGenerator;
    private FactoryComponent m_Factory;
    [SerializeField]
    private int m_HiddenLayers;
    private float m_ProcessThoughtRandomnessRange = 1f;
    private int thoughtProcessSize;

    void Start()
    {
        collection.agentCompleteEvent += AgentComplete;
        m_Factory = GetComponent<FactoryComponent>();
        m_Factory.gameObjectSpawnedEvent += InitializeSpawn;
        m_NeuralNetworkGeneticGenerator.SetupForUse(GetThoughtProcessSize());
        m_NeuralNetworkGeneticGenerator.CreateInitialThoughtProcesses(GetThoughtProcessSize(), m_ProcessThoughtRandomnessRange);
        m_Factory.SpawnAll();
    }

    public int GetThoughtProcessSize()
    {
        if (thoughtProcessSize == 0)
        {
            int output = m_Factory.prefabToSpawn.GetComponentsInChildren<INeuralNetworkOutputNode>().Count();
            thoughtProcessSize = m_HiddenLayers + // Bias
                (m_HiddenLayers * m_Factory.prefabToSpawn.GetComponentsInChildren<INeuralNetworkInputNode>().Count()) + // Weights 
                output + //Bias 
                (output * m_HiddenLayers); // Weights 
        }

        return thoughtProcessSize;
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
        nn.Initialize(m_HiddenLayers, thoughtProcess);
    }
}
