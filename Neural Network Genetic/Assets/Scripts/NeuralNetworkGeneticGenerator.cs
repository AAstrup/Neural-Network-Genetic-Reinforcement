using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Generates the genetics for the NNgeneticfactory
/// </summary>
public class NeuralNetworkGeneticGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Pair> bestThoughtProcesses;
    public bool _IsLearning = true;
    private TextEditor textEditor = new TextEditor();
    private StringBuilder stringBuilder = new StringBuilder();
    public int topAmountToUse = 2;
    public int randomPercentage = 5;
    public int mutatePercentage = 5;
    private int maxProcessSizeToTweak = 8;
    [SerializeField]
    private float m_MinimumFitnessCandidateDistinction = 10f;
    private System.Random random;

    private void Awake()
    {
        random = new System.Random();
        foreach (var item in bestThoughtProcesses)
        {
            item.process.CompleteInspectorInjected();
        }
    }

    public void SetupForUse(int thoughtProcessSize)
    {
        maxProcessSizeToTweak = thoughtProcessSize;
    }

    public void CreateInitialThoughtProcesses(int thoughtProcessSize, float processThoughtRandomnessRange)
    {
        for (int i = bestThoughtProcesses.Count; i < topAmountToUse; i++)
        {
            var process = new ThoughtProcess(thoughtProcessSize, processThoughtRandomnessRange);
            bestThoughtProcesses.Add(new Pair() { process = process });
        }
    }

    public void AgentComplete(NeuralNetworkFittnessSum nnFitness)
    {
        if (_IsLearning)
        {
            if (nnFitness.GetFitness() > bestThoughtProcesses[bestThoughtProcesses.Count - 1].fitness && !bestThoughtProcesses.Exists(x => m_MinimumFitnessCandidateDistinction > Mathf.Abs(x.fitness - nnFitness.GetFitness())))
            {
                bestThoughtProcesses.RemoveAt(bestThoughtProcesses.Count - 1);
                bestThoughtProcesses.Add(new Pair() { process = nnFitness.nn.thoughtProcess, fitness = nnFitness.GetFitness() });
                bestThoughtProcesses = bestThoughtProcesses.OrderByDescending(x => x.fitness).ToList();

                foreach (var item in bestThoughtProcesses)
                {
                    stringBuilder.AppendLine(item.process.GetThoughtAsString());
                }
                textEditor.text = stringBuilder.ToString();
                textEditor.SelectAll();
                textEditor.Copy();
            }
            Debug.Log("Final List");
            for (int i = 0; i < bestThoughtProcesses.Count; i++)
            {
                Debug.Log("bestThoughtProcesses " + i + " with fitness of " + bestThoughtProcesses[i].fitness);
            }
        }
    }

    public ThoughtProcess ProduceChild(int thoughtProcessSize, float processThoughtRandomnessRange)
    {
        var chance = UnityEngine.Random.Range(0, 100);
        if (chance < randomPercentage)
        {
            return new ThoughtProcess(thoughtProcessSize, processThoughtRandomnessRange);
        }
        else
        {
            int firstParent = random.Next(topAmountToUse);
            int secondParent = 0;
            while (topAmountToUse > 1 && firstParent == secondParent)
            {
                secondParent = random.Next(topAmountToUse);
            }
            var child = new ThoughtProcess(bestThoughtProcesses[firstParent].process, bestThoughtProcesses[secondParent].process);

            if (chance < randomPercentage + mutatePercentage)
            {
                Tweak(child, thoughtProcessSize, processThoughtRandomnessRange);
                return child;
            }
            else
                return child;
        }
    }

    private void Tweak(ThoughtProcess thoughtProcesses, int thoughtProcessSize, float processThoughtRandomnessRange)
    {
        for (int x = 0; x < maxProcessSizeToTweak; x++)
        {
            thoughtProcesses.biasAndWeights[random.Next(thoughtProcessSize - 1)] = random.Next(Mathf.RoundToInt(processThoughtRandomnessRange));
        }
    }
}
