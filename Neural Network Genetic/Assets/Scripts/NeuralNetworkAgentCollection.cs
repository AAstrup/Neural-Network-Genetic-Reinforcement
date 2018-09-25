using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkAgentCollection : MonoBehaviour {
    [HideInInspector]
    public delegate void AgentCompleteEvent(NeuralNetworkFittnessSum nnFitness);
    public AgentCompleteEvent agentCompleteEvent;
    public List<GameObject> spawns;

    void Awake () {
        spawns = new List<GameObject>();
        foreach (var factory in GetComponentsInChildren<FactoryComponent>())
        {
            factory.gameObjectSpawnedEvent += OnSpawn;
        }
    }

    private void OnSpawn(GameObject spawn)
    {
        spawns.Add(spawn);
        var nn = spawn.GetComponent<NeuralNetworkFittnessSum>();
        if (nn != null)
        {
            nn.completedTest += AgentComplete;
        }
    }

    private void AgentComplete(NeuralNetworkFittnessSum nnFitness, float fitness)
    {
        spawns.Remove(nnFitness.gameObject);
        if (agentCompleteEvent != null)
        {
            agentCompleteEvent(nnFitness);
        }
    }
}
