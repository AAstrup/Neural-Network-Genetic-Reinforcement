using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sums fitness of components with INNFitnessCalculator on this GameObject and childs
/// </summary>
[RequireComponent(typeof(NeuralNetwork), typeof(RunComponent))]
public class NeuralNetworkFittnessSum : MonoBehaviour, INNResetable {
    [SerializeField]
    private float m_Fitness = 0f;
    public delegate void CompletedTest(NeuralNetworkFittnessSum nnFitness, float fitness);
    public CompletedTest completedTest;
    [HideInInspector]
    private bool completed;
    [HideInInspector]
    public NeuralNetwork nn;

    private void Awake()
    {
        nn = GetComponent<NeuralNetwork>();
    }

    public float GetFitness()
    {
        if (!completed)
            throw new Exception("Fitness not evaluated yet!");
        else
            return m_Fitness;
    }

    public void NNReset()
    {
        var fitnessComps = GetComponentsInChildren<INNFitnessCalculator>();
        foreach (var item in fitnessComps)
        {
            m_Fitness += item.GetFitness();
        }
        m_Fitness /= fitnessComps.Length;
        completed = true;
        if (completedTest != null)
        {
            completedTest(this, m_Fitness);
        }

        m_Fitness = 0f;
        completed = false;
    }
}
