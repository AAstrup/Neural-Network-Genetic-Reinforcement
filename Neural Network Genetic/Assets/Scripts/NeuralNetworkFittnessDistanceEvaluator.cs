﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NeuralNetwork), typeof(RunComponent))]
public class NeuralNetworkFittnessDistanceEvaluator : MonoBehaviour, INNResetable {
    [SerializeField]
    private float m_Fitness = 0f;
    public delegate void CompletedTest(NeuralNetworkFittnessDistanceEvaluator nnFitness, float fitness);
    public CompletedTest completedTest;
    public bool completed;
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