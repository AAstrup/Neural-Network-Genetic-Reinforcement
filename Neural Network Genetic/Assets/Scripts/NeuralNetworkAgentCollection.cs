using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FactoryComponent))]
public class NeuralNetworkAgentCollection : MonoBehaviour {
    [HideInInspector]
    public delegate void AgentCompleteEvent(NeuralNetworkFittnessSum nnFitness);
    public AgentCompleteEvent agentCompleteEvent;

    void Awake () {
        GetComponent<FactoryComponent>().gameObjectSpawnedEvent += OnSpawn;
    }

    private void OnSpawn(GameObject spawn)
    {
        var nn = spawn.GetComponent<NeuralNetworkFittnessSum>();
        if (nn != null)
        {
            nn.completedTest += AgentComplete;
        }
    }

    private void AgentComplete(NeuralNetworkFittnessSum nnFitness, float fitness)
    {
        if (agentCompleteEvent != null)
        {
            agentCompleteEvent(nnFitness);
        }
    }
}
